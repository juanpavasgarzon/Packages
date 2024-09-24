using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pavas.Patterns.UnitOfWork.Abstracts.Extensions;
using Pavas.Patterns.UnitOfWork.Contracts;
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
    private readonly string _tenantId;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class using EF Core options.
    /// </summary>
    /// <param name="contextOptions">The options used by the database context for configuration.</param>
    protected DatabaseContext(DbContextOptions contextOptions)
    {
        var extension = contextOptions.FindExtension<DatabaseOptionsExtension>();
        var options = extension?.GetDatabaseOptions();
        if (options is null)
            throw new InvalidOperationException("DatabaseOptionsExtension is required");

        _connectionString = options.ConnectionString;
        _tenantId = options.TenantId;
        _softDelete = options.SoftDelete;

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
        modelBuilder.HasQueryFilter<ISoftDelete>(e => !e.IsDeleted);
        modelBuilder.HasQueryFilter<ITenancy>(e => e.TenantId == _tenantId);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Handles state changes for base entities by applying tenant, timestamp, and soft delete rules.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">The event arguments related to the state changes of the entity entry.</param>
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

        if (string.IsNullOrEmpty(_tenantId) || string.IsNullOrWhiteSpace(_tenantId))
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
}