using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pavas.Patterns.UnitOfWork.Abstracts.Extensions;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.Options.Extensions;

namespace Pavas.Patterns.UnitOfWork.Abstracts;

/// <summary>
/// Abstract base class for configuring a database context in EF Core, with support for soft delete and tenant management.
/// </summary>
public abstract class DatabaseContext : DbContext
{
    private readonly IDatabaseOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class with EF Core options.
    /// </summary>
    /// <param name="contextOptions">The options used by the database context.</param>
    protected DatabaseContext(DbContextOptions contextOptions)
    {
        var extension = contextOptions.FindExtension<DatabaseOptionsExtension>();
        var options = extension?.GetDatabaseOptions();
        _options = options ?? throw new InvalidOperationException("DatabaseOptionsExtension is required");
        AddChangeTrackerEvents();
    }

    /// <summary>
    /// Adds event handlers to the ChangeTracker to handle soft delete and base entity modifications.
    /// </summary>
    private void AddChangeTrackerEvents()
    {
        if (!_options.SoftDelete)
        {
            base.ChangeTracker.StateChanged += SoftDeleteEvent;
            base.ChangeTracker.Tracked += SoftDeleteEvent;
        }

        base.ChangeTracker.StateChanged += BaseEntityEvent;
        base.ChangeTracker.Tracked += BaseEntityEvent;
    }

    /// <summary>
    /// Retrieves the database options configuration.
    /// </summary>
    /// <returns>An instance of <see cref="IDatabaseOptions"/> containing the configuration.</returns>
    protected IDatabaseOptions GetDatabaseOptions()
    {
        return _options;
    }

    /// <summary>
    /// Defines the database provider using the provided connection string.
    /// </summary>
    /// <param name="optionsBuilder">The builder used to configure the database context.</param>
    /// <param name="connectionString">The connection string for the database.</param>
    protected abstract void GetProvider(DbContextOptionsBuilder optionsBuilder, string connectionString);

    /// <summary>
    /// Configures the database context using the provider and connection string.
    /// </summary>
    /// <param name="optionsBuilder">The builder used to configure the database context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        GetProvider(optionsBuilder, _options.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Configures the model by applying global filters and loading configurations from the assembly.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the database.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasQueryFilter<IEntity>(e => e.TenantId == "Default");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Event handler for base entity changes such as creation and modification.
    /// Automatically updates timestamps and tenant information.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">Event arguments related to the entity entry.</param>
    private void BaseEntityEvent(object? sender, EntityEntryEventArgs args)
    {
        if (args is not EntityStateChangedEventArgs or EntityTrackedEventArgs)
            return;

        if (args.Entry.State is not (EntityState.Added or EntityState.Modified))
            return;

        if (args.Entry.Entity is not IEntity entity)
            return;

        switch (args.Entry.State)
        {
            case EntityState.Modified:
                entity.UpdatedAt = DateTime.UtcNow;
                break;

            case EntityState.Added:
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.TenantId = _options.DefaultTenant;
                break;

            case EntityState.Deleted:
            case EntityState.Detached:
            case EntityState.Unchanged:
            default:
                break;
        }
    }

    /// <summary>
    /// Event handler for soft delete logic. Converts a deletion into an update by setting a deletion timestamp.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">Event arguments related to the entity entry.</param>
    private static void SoftDeleteEvent(object? sender, EntityEntryEventArgs args)
    {
        if (args is not EntityStateChangedEventArgs or EntityTrackedEventArgs)
            return;

        if (args.Entry.State is not EntityState.Deleted)
            return;

        if (args.Entry.Entity is not ISoftDelete entity)
            return;

        entity.DeletedAt = DateTime.UtcNow;
        args.Entry.State = EntityState.Modified;
    }
}