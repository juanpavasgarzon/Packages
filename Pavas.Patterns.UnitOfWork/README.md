# Pavas.Patterns.UnitOfWork Library

## Overview

This library provides a Unit of Work and Repository pattern implementation for managing database interactions using
Entity Framework Core. It includes support for soft delete, tenant management, correlation IDs, and configurable
repository options. Below is the detailed documentation for each part of the library.

## Table of Contents

- [DatabaseContext](#databasecontext)
- [Repository](#repository)
- [UnitOfWork](#unitofwork)
- [Contracts](#contracts)
    - [ICorrelated](#icorrelated)
    - [IDatabaseOptions](#idatabaseoptions)
    - [ISoftDelete](#isoftdelete)
    - [ITenancy](#itenancy)
    - [ITimestamps](#itimestamps)
    - [IUnitOfWork](#iunitofwork)

## DatabaseContext

### Summary

The `DatabaseContext` class is an abstract base class for configuring a database context in EF Core. It includes
mechanisms for handling soft delete, multi-tenancy, and auditing (timestamps).

### Example Usage

```csharp
public class MyDbContext : DatabaseContext
{
    protected override void GetProvider(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}
```

## Repository

### Summary

The `Repository<TEntity>` class is a generic repository for managing data access for entities. It includes methods for
retrieving, adding, updating, and removing entities asynchronously. It is automatically provided through the
`UnitOfWork`, and you do not need to implement it manually.

### Example Usage

```csharp
var repository = await unitOfWork.GetRepositoryAsync<MyEntity>();
var entity = await repository.GetByIdAsync(1);
await repository.AddAsync(new MyEntity { Name = "New Entity" });
```

## UnitOfWork

### Summary

The `UnitOfWork` class provides a mechanism for managing transactions and repositories within a database context. It
allows for retrieving repositories, managing database transactions, and saving changes. You do not need to manually
implement repositories; `UnitOfWork` provides them.

### Example Usage

```csharp
var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
var repository = await unitOfWork.GetRepositoryAsync<MyEntity>();
await unitOfWork.SaveChangesAsync();
```

## Contracts

### ICorrelated

Defines an entity that is associated with a correlation ID for tracking related actions.

#### Example

```csharp
public class MyEntity : ICorrelated
{
    public string CorrelationId { get; set; }
}
```

### IDatabaseOptions

Defines the configuration options for the database, including the connection string and soft delete settings.

#### Example

```csharp
public class MyDatabaseOptions : IDatabaseOptions
{
    public string ConnectionString { get; set; } = "your-connection-string";
    public string TenantId { get; set; } = "your-connection-string";
    public string CorrelationId { get; set; } = "6e343h3f3434h3jj3434v3ggg34"; // use in scope or transient injection
    public bool SoftDelete { get; set; } = true;
    public bool EnsureCreated { get; set; } = true;
}
```

### ISoftDelete

Represents an entity that supports soft delete functionality by maintaining a deletion timestamp.

#### Example

```csharp
public class MyEntity : ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
```

### ITenancy

Represents an entity associated with a tenant in a multi-tenant system.

#### Example

```csharp
public class MyEntity : ITenancy
{
    public string TenantId { get; set; }
}
```

### ITimestamps

Represents an entity with common properties for auditing and tenant management.

#### Example

```csharp
public class MyEntity : ITimestamps
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

### IUnitOfWork

Defines a contract for the UnitOfWork pattern, including methods for retrieving repositories and managing transactions.

#### Example

```csharp
var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
var repository = await unitOfWork.GetRepositoryAsync<MyEntity>();
await unitOfWork.SaveChangesAsync();
```

