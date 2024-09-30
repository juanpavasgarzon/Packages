# Pavas.Patterns.UnitOfWork Library

## Overview

This library provides a Unit of Work and Repository pattern implementation for managing database interactions using
Entity Framework Core. It includes support for soft delete, tenant management, correlation IDs, and configurable
repository options. Below is the detailed documentation for each part of the library.

## DatabaseContext

### Summary

The `DatabaseContext` class is an abstract base class for configuring a database context in EF Core. It includes
mechanisms for handling soft delete, multi-tenancy, and auditing (timestamps).

## Repository

### Summary

The `Repository<TEntity>` class is a generic repository for managing data access for entities. It includes methods for
retrieving, adding, updating, and removing entities asynchronously. It is automatically provided through the
`UnitOfWork`, and you do not need to implement it manually.

## UnitOfWork

### Summary

The `IUnitOfWork` interface provides a mechanism for managing transactions and repositories within a database context.
It
allows for retrieving repositories, managing database transactions, and saving changes. You do not need to manually
implement repositories; `IUnitOfWork` provides them.

## Contracts

### ICorrelated / BaseCorrelated

Defines an entity that is associated with a correlation ID for tracking related actions.

### ISoftDelete / BaseSoftDelete

Represents an entity that supports soft delete functionality by maintaining a deletion timestamp.

### ITenancy / BaseTenancy

Represents an entity associated with a tenant in a multi-tenant system.

### ITimestamps / BaseTimestamps

Represents an entity with common properties for auditing dates.

### BaseEntity

Represents an abstraction entity definition, implements:

* `ITimestamps`
* `ICorrelated`
* `ISoftDelete`
* `ITenancy`

## Database

### IDatabaseOptions / IDatabaseConfigurator

Defines the configuration options for the database, including the connection string, tenant, correlation and other
database options

### IRepositoryOptions / IRepositoryConfigurator

Defines the configuration options for the repository, including the tenant and correlation, this options overrides
database same attributes options

## Examples

### Database context

Extends of `DatabaseContext` abstraction

```csharp
public sealed class MyDatabaseContext(DbContextOptions contextOptions) : DatabaseContext(contextOptions)
{
    public required DbSet<MyEntity> MyEntity { get; set; }

    protected override void GetProvider(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        var version = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, version, builder => { builder.EnableRetryOnFailure(3); });
    }
}
```

#### Database configurator

In this process you can define a strategy for access a connection and tenant values, provide a service provider tool,
implements `IDatabaseConfigurator` interface

```csharp
public sealed class MyDatabaseConfigurator : IDatabaseConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        databaseOptions.ConnectionString = "Host=localhost;Database=MyDatabase;Username=root;Password=root";
        databaseOptions.TenantId = "Default";
        databaseOptions.CorrelationId = Guid.NewGuid().ToString();
        databaseOptions.EnsureCreated = true;
        return databaseOptions;
    }
}
```

#### Service register

Register unit of work service and define injection form with `ServiceLifetime`

* With configuration

```csharp
builder.Services.AddUnitOfWork<MyDatabaseContext, MyDatabaseConfigurator>(ServiceLifetime.Scoped);
```

* With options

```csharp
builder.Services.AddUnitOfWork<MyDatabaseContext>(options => {
    options.ConnectionString = "Host=localhost;Database=MyDatabase;Username=root;Password=root";
    options.TenantId = "Default";
    options.CorrelationId = Guid.NewGuid().ToString();
    options.EnsureCreated = true;
}, ServiceLifetime.Scoped);
```

#### Unit of work

Get unit of work pattern implementation

```csharp
var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
```

#### Repository

Get repository for entity with unit of work, you can override database attributes with `IRepositoryConfigurator`
or `IRepositoryOptions`

* Default

```csharp
var repository = await unitOfWork.GetRepository<MyEntity>();
```

* Override with options

```csharp
var repository = await unitOfWork.GetRepository<MyEntity>(options => {
    options.TenantId = "Other tenant";
    options.CorrelationId = "Other correlation";
});
```

* Override with configurator

```csharp
public sealed class MyRepositoryConfigurator 
{
    public IRepositoryOptions Configure(IServiceProvider serviceProvider, IRepositoryOptions repositoryOptions) {
        repositoryOptions.TenantId = "Other tenant";
        repositoryOptions.CorrelationId = "Other correlation";

        return repositoryOptions;
    }
}
```

#### Transactions

Use `ExecutionStrategyAsync` method to use database transactions

```csharp
await unitOfWork.ExecutionStrategyAsync(async transaction => {
    var repository = unitOfWork.GetRepository<MyEntity>();

    try
    {
        await repository.AddAsync(new MyEntity(), stoppingToken);
        await repository.AddAsync(new MyEntity(), stoppingToken);
        await repository.AddAsync(new MyEntity(), stoppingToken);
        await repository.AddAsync(new MyEntity(), stoppingToken);
        await unitOfWork.SaveChangesAsync(stoppingToken);
    
        await repository.AddAsync(new MyEntity(), stoppingToken);
        await repository.AddAsync(new MyEntity(), stoppingToken);
        await repository.AddAsync(new MyEntity(), stoppingToken);
        await repository.AddAsync(new MyEntity(), stoppingToken);
    
        await unitOfWork.SaveChangesAsync(stoppingToken);
        await transaction.CommitAsync(stoppingToken);
    }
    catch (Exception e)
    {
        await transaction.RollbackAsync(stoppingToken);
        Console.WriteLine(e);
    }
    }, stoppingToken);
```

```csharp
var repository = await unitOfWork.GetRepository<MyEntity>, MyRepositoryConfigurator();
```

#### Repository methods

```csharp
public Task<TEntity?> GetByKeyAsync<TKey>(TKey key, CancellationToken token = new()) where TKey : notnull;
public Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = new());
public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = new());
public Task<IQueryable<TEntity>> GetQueryAsync(CancellationToken token = new());
public Task<EntityEntry<TEntity>> AddAsync(TEntity entry, CancellationToken token = new());
public Task AddManyAsync(IEnumerable<TEntity> entries, CancellationToken token = new());
public Task<EntityEntry<TEntity>> UpdateAsync(TEntity entry, CancellationToken token = new());
public Task<EntityEntry<TEntity>> RemoveByKeyAsync<TKey>(TKey key, CancellationToken token = new())
public Task<EntityEntry<TEntity>> RemoveAsync(TEntity entry, CancellationToken token = new());
public Task RemoveManyAsync(IEnumerable<TEntity> entries, CancellationToken token = new());
public TEntity? GetByKey<TKey>(TKey key) where TKey : notnull;
public TEntity? GetOne(Expression<Func<TEntity, bool>> filter);
public IEnumerable<TEntity> GetAll();
public IQueryable<TEntity> GetQuery();
public EntityEntry<TEntity> Add(TEntity entry);
public void AddMany(IEnumerable<TEntity> entries);
public EntityEntry<TEntity> Update(TEntity entry);
public EntityEntry<TEntity> RemoveByKey<TKey>(TKey key) where TKey : notnull;
public EntityEntry<TEntity> Remove(TEntity entry);
public void RemoveMany(IEnumerable<TEntity> entries);
```

#### Unit of work methods

```csharp
public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
public IRepository<TEntity> GetRepository<TEntity>(Action<IRepositoryOptions> configure)
public IRepository<TEntity> GetRepository<TEntity, TConfigurator>() where TEntity : class
public Task ExecutionStrategyAsync(Func<IDbContextTransaction, Task> operation, CancellationToken token = new());
public void ExecutionStrategy(Action<IDbContextTransaction> operation);
public Task<int> SaveChangesAsync(CancellationToken token = new());
public int SaveChanges();
```
