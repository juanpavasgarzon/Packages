# Pavas Patterns - Unit of Work Library

This repository contains an implementation of the Unit of Work design pattern along with a repository pattern, soft
delete functionality, multi-tenancy, and timestamp auditing for Entity Framework Core (EF Core). The library is designed
to help manage data access and context lifecycle more efficiently, by handling common database operations such as
creation, modification, and soft deletion of entities.

## Features

- **Unit of Work pattern**: Centralized management of database transactions and repositories.
- **Repository pattern**: Encapsulates data access logic and simplifies querying and manipulation of entities.
- **Soft Delete**: Allows entities to be "soft deleted", marking them as deleted without removing them from the
  database.
- **Multi-tenancy**: Provides support for managing entities across different tenants.
- **Timestamp Auditing**: Automatically tracks entity creation and update times.

## Usage

### Database Context

The `DatabaseContext` class is an abstract base class that configures the EF Core context with support for:

- Soft delete (`ISoftDelete`)
- Multi-tenancy (`ITenancy`)
- Timestamps (`ITimestamps`)

Example implementation:

```csharp
public class MyDbContext : DatabaseContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<MyEntity> MyEntities { get; set; }
}
```

### Unit of Work

The `IUnitOfWork` interface provides methods to manage transactions and retrieve repositories. It ensures that all
operations are managed within a single transaction scope.

Example usage:

```csharp
public class MyService
{
    private readonly IUnitOfWork _unitOfWork;

    public MyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MyEntity> GetEntityAsync(int id)
    {
        var repository = _unitOfWork.GetRepository<MyEntity>();
        return await repository.GetByIdAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveChangesAsync();
    }
}
```

### Repository Pattern

The `IRepository<TEntity>` interface defines the repository pattern for CRUD operations on entities.

```csharp
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}
```

### Soft Delete

Entities implementing `ISoftDelete` will automatically support soft delete operations.

```csharp
public class MyEntity : ISoftDelete
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
```

### Multi-tenancy

Entities implementing `ITenancy` will have a `TenantId` that can be used to manage data across different tenants.

```csharp
public class MyEntity : ITenancy
{
    public string TenantId { get; set; }
}
```

## Dependency Injection

To register the `UnitOfWork` and `DbContext`, you can use the provided `AddUnitOfWork` extension methods.

```csharp
services.AddUnitOfWork<MyDbContext>(options =>
{
    options.ConnectionString = "your-connection-string";
    options.SoftDelete = true;
    options.TenantId = "default-tenant";
}, ServiceLifetime.Scoped);
```

## License

This project is licensed under the MIT License.
