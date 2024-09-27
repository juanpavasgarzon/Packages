using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.Extensions;
using Pavas.Patterns.UnitOfWork.Options.Extensions;

namespace Pavas.Patterns.UnitOfWork.Abstracts;

/// <summary>
/// Abstract base class for configuring a database context in EF Core, with support for soft delete, timestamps, and tenant management.
/// Provides mechanisms for automatically handling entity state changes for common patterns such as 
/// soft delete, multi-tenancy, and auditing (timestamps).
/// </summary>
public abstract class DatabaseContext : DbContext
{
    private readonly string _connectionString;
    private readonly bool _softDelete;
    private string _tenantId;
    private string _correlationId;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class using EF Core options.
    /// </summary>
    /// <param name="contextOptions">The options used by the database context for configuration.</param>
    protected DatabaseContext(DbContextOptions contextOptions)
    {
        var extension = contextOptions.FindExtension<DatabaseOptionsExtension>();
        if (extension is null)
            throw new InvalidOperationException("DatabaseOptionsExtension is required");

        extension.Validate(contextOptions);
        var options = extension.GetDatabaseOptions();

        _connectionString = options.ConnectionString;
        _softDelete = options.SoftDelete;
        _tenantId = options.TenantId;
        _correlationId = options.CorrelationId;

        AddEntityEvents();
        if (!options.EnsureCreated)
            return;

        base.Database.EnsureCreated();
    }

    /// <summary>
    /// Adds event handlers to the ChangeTracker to handle soft delete, timestamps, and tenant information automatically.
    /// </summary>
    private void AddEntityEvents()
    {
        base.ChangeTracker.StateChanged += EventByEntityType;
        base.ChangeTracker.Tracked += EventByEntityType;
    }

    /// <summary>
    /// Defines the database provider using the connection string provided in the options.
    /// </summary>
    /// <param name="optionsBuilder">The builder used to configure the database context.</param>
    /// <param name="connectionString">The connection string for the database provider.</param>
    protected abstract void GetProvider(DbContextOptionsBuilder optionsBuilder, string connectionString);

    /// <summary>
    /// Configures the database context using the provider and connection string from the database options.
    /// </summary>
    /// <param name="optionsBuilder">The builder used to configure the database context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        GetProvider(optionsBuilder, _connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Configures the entity model by applying global query filters for tenancy and soft delete,
    /// and loading entity configurations from the current assembly.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the entity model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var clrType in modelBuilder.Model.GetEntityTypes().Select(type => type.ClrType))
        {
            var parameter = Expression.Parameter(clrType, "e");
            var filter = GetGlobalFilters(parameter, clrType);
            if (filter is null)
                continue;

            var lambda = Expression.Lambda(filter, parameter);
            modelBuilder.Entity(clrType).HasQueryFilter(lambda);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Generates global query filters for entities based on the interfaces they implement.
    /// If the entity type implements <see cref="ISoftDelete"/>, a filter is applied to exclude soft-deleted entities.
    /// If the entity type implements <see cref="ITenancy"/>, a filter is applied to include only entities that match the specified tenant.
    /// </summary>
    /// <param name="parameter">The <see cref="ParameterExpression"/> representing the entity in the expression.</param>
    /// <param name="clrType">The CLR type of the entity being processed.</param>
    /// <returns>
    /// An <see cref="Expression"/> that combines the applicable filters using a logical AND, or null if no filters are applicable.
    /// </returns>
    [SuppressMessage("ReSharper", "InvertIf")]
    private Expression? GetGlobalFilters(ParameterExpression parameter, Type clrType)
    {
        var filters = new List<Expression>();

        if (typeof(ISoftDelete).IsAssignableFrom(clrType))
        {
            var softDeleteProperty = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
            filters.Add(Expression.Not(softDeleteProperty));
        }

        if (typeof(ITenancy).IsAssignableFrom(clrType))
        {
            var tenantProperty = Expression.Property(parameter, nameof(ITenancy.TenantId));
            var tenantValue = Expression.Constant(_tenantId, typeof(string));
            filters.Add(Expression.Equal(tenantProperty, tenantValue));
        }

        return filters.Count > 0 ? filters.Aggregate(Expression.AndAlso) : null;
    }

    /// <summary>
    /// Handles state changes for base entities by applying tenant, timestamp, and soft delete rules.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">The event arguments related to the state changes of the entity entry.</param>
    [SuppressMessage("ReSharper", "InvertIf")]
    private void EventByEntityType(object? sender, EntityEntryEventArgs args)
    {
        if (args is not (EntityStateChangedEventArgs or EntityTrackedEventArgs))
            return;

        if (args.Entry.Entity is ITimestamps timestampsEntity)
            Timestamps(timestampsEntity, args);

        if (args.Entry.Entity is ITenancy tenancyEntity)
            Tenancy(tenancyEntity, args);

        if (args.Entry.Entity is ISoftDelete softDeleteEntity)
            SoftDelete(softDeleteEntity, args);

        if (args.Entry.Entity is ICorrelated correlatedEntity)
            Correlated(correlatedEntity, args);
    }

    /// <summary>
    /// Applies timestamp management by setting creation and update times when entities are added or modified.
    /// </summary>
    /// <param name="entity">The entity implementing <see cref="ITimestamps"/>.</param>
    /// <param name="args">The current event arguments related to the entity entry.</param>
    private static void Timestamps(ITimestamps entity, EntityEntryEventArgs args)
    {
        if (args.Entry.State is not (EntityState.Added or EntityState.Modified))
            return;

        switch (args.Entry.State)
        {
            case EntityState.Added:
                entity.CreatedAt = DateTime.UtcNow;
                break;
            case EntityState.Modified:
                entity.UpdatedAt = DateTime.UtcNow;
                break;
            case EntityState.Detached:
            case EntityState.Unchanged:
            case EntityState.Deleted:
            default:
                break;
        }
    }

    /// <summary>
    /// Applies tenant management logic by setting the default tenant ID when a new entity is added.
    /// </summary>
    /// <param name="entity">The entity implementing <see cref="ITenancy"/>.</param>
    /// <param name="args">The current event arguments related to the entity entry.</param>
    /// <exception cref="ArgumentException">Thrown when the TenantId is not provided in the options.</exception>
    private void Tenancy(ITenancy entity, EntityEntryEventArgs args)
    {
        if (args.Entry.State is not EntityState.Added)
            return;

        if (_tenantId.IsNullOrEmptyOrWhiteSpace())
            throw new ArgumentException("Tenant Id is required when using ITenancy implementation");

        entity.TenantId = _tenantId;
    }

    /// <summary>
    /// Applies soft delete logic by setting the deletion timestamp and marking the entity as deleted.
    /// </summary>
    /// <param name="entity">The entity implementing <see cref="ISoftDelete"/>.</param>
    /// <param name="args">The current event arguments related to the entity entry.</param>
    private void SoftDelete(ISoftDelete entity, EntityEntryEventArgs args)
    {
        if (!_softDelete)
            return;

        switch (args.Entry.State)
        {
            case EntityState.Added:
                entity.IsDeleted = false;
                entity.DeletedAt = null;
                break;
            case EntityState.Deleted:
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                args.Entry.State = EntityState.Modified;
                break;
            case EntityState.Detached:
            case EntityState.Unchanged:
            case EntityState.Modified:
            default:
                break;
        }
    }

    /// <summary>
    /// Applies correlation ID logic to an entity based on its current state.
    /// When the entity is in the Added or Modified state, it assigns the current correlation ID.
    /// </summary>
    /// <param name="entity">The entity implementing <see cref="ICorrelated"/> that requires a correlation ID.</param>
    /// <param name="args">The event arguments containing information about the entity's entry and its state.</param>
    private void Correlated(ICorrelated entity, EntityEntryEventArgs args)
    {
        if (_correlationId.IsNullOrEmptyOrWhiteSpace())
            throw new ArgumentException("Correlation Id is required when using ICorrelated implementation");

        switch (args.Entry.State)
        {
            case EntityState.Added:
            case EntityState.Modified:
                entity.CorrelationId = _correlationId;
                break;
            case EntityState.Detached:
            case EntityState.Deleted:
            case EntityState.Unchanged:
            default:
                break;
        }
    }

    /// <summary>
    /// Sets the correlation ID for the current instance, allowing related operations or events
    /// to be tracked under a common identifier.
    /// </summary>
    /// <param name="correlationId">The correlation ID to associate with this instance, used for tracking operations.</param>
    public void SetCorrelationId(string correlationId)
    {
        if (correlationId.IsNullOrEmptyOrWhiteSpace())
            throw new ArgumentException("Value is required when you set the correlation ID");

        _correlationId = correlationId;
    }

    /// <summary>
    /// Sets the tenant ID for the current instance, allowing related operations or events
    /// to be tracked under a common identifier.
    /// </summary>
    /// <param name="tenantId">The tenant ID to associate with this instance.</param>
    public void SetTenantId(string tenantId)
    {
        if (tenantId.IsNullOrEmptyOrWhiteSpace())
            throw new ArgumentException("Value is required when you set the tenant ID");

        _tenantId = tenantId;
    }
}