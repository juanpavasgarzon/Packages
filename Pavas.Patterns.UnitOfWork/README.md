
# Pavas Patterns Unit of Work Library

## Overview

This library implements the **Unit of Work** design pattern, allowing the management of database transactions across multiple operations and ensuring consistency in data persistence. It's designed to work in combination with the **Repository Pattern** and supports features like **soft delete** and **tenant management**.

### Key Features
- **Repository Pattern**: Repositories abstract the data access logic for entities, providing a clean interface for database operations.
- **Unit of Work Pattern**: Manages transaction boundaries, ensuring that changes across multiple repositories are treated as a single transaction.
- **Soft Delete**: Entities can be marked as deleted without being physically removed from the database, preserving data integrity.
- **Tenant Management**: Supports multi-tenancy, allowing entities to be associated with specific tenants.

## Contracts

### IRepository<TEntity>
Defines the operations available for working with entities in the database. Operations include retrieving, adding, updating, and removing entities.

#### Methods:
- **GetByIdAsync(int id)**: Retrieve an entity by its ID.
- **GetOneAsync(Expression<Func<TEntity, bool>> filter)**: Retrieve a single entity matching the filter.
- **GetAllAsync(Expression<Func<TEntity, bool>> filter)**: Retrieve a collection of entities matching the filter.
- **GetQueryAsync()**: Retrieve an IQueryable for querying entities.
- **AddAsync(TEntity entry)**: Add a new entity to the database.
- **AddManyAsync(IEnumerable<TEntity> entries)**: Add multiple entities to the database.
- **UpdateAsync(TEntity entry)**: Update an existing entity.
- **RemoveAsync(TEntity entry)**: Remove an entity.
- **RemoveManyAsync(IEnumerable<TEntity> entries)**: Remove multiple entities.

### ITenancy
Represents a base entity that includes metadata like `TenantId`. This interface ensures that all entities have tenant information.

### ITimestamps
Represents a base entity that includes metadata like `CreatedAt` and `UpdatedAt` This interface ensures that all entities have basic auditing and tenant information.

### ISoftDelete
Provides support for soft delete functionality by including a `DeletedAt` timestamp.

### IUnitOfWork
Manages the lifecycle of repositories and transactions. It handles the commit of changes to the database and supports rollback in case of errors.

#### Methods:
- **GetRepository<TEntity>()**: Retrieves the repository for the specified entity type.
- **BeginTransactionAsync()**: Begins a database transaction.
- **SaveChangesAsync()**: Saves all changes to the database.

### IDatabaseOptions
Defines the configuration options for the database, including connection string, tenant ID, and soft delete support.

## Usage

### Configuring Unit of Work

To configure the Unit of Work in your project, register it in the dependency injection container. You can configure it either with or without access to the `IServiceProvider`.

```csharp
// Registering UnitOfWork with options configuration
services.AddUnitOfWork<YourDbContext>(options =>
{
    options.ConnectionString = "your-connection-string";
    options.TenantId = "MyTenant";
    options.SoftDelete = true;
});
```

### Example Entity
```csharp
public class Customer : IEntity, ISoftDelete
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string TenantId { get; set; }
    public DateTime DeletedAt { get; set; }
}
```

### Example Repository
```csharp
public class CustomerRepository : IRepository<Customer>
{
    private readonly DbContext _context;

    public CustomerRepository(DbContext context)
    {
        _context = context;
    }

    public Task<Customer?> GetByIdAsync(int id) => _context.Set<Customer>().FindAsync(id).AsTask();

    // Implement other methods based on the IRepository contract
}
```

## Soft Delete
When soft delete is enabled, entities implementing `ISoftDelete` will have their `DeletedAt` timestamp set instead of being removed from the database.

## License
This library is licensed under the MIT License.
