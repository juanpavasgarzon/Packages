using System.Linq.Expressions;

namespace Pavas.Patterns.UnitOfWork.Contracts;

/// <summary>
/// Defines a generic repository pattern for handling data access operations on entities
/// </summary>
/// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Asynchronously retrieves an entity from the context based on its primary key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identify the entity.</typeparam>
    /// <param name="key">The key of the entity to retrieve.</param>
    /// <returns>
    /// A <see cref="Task{TEntity}"/> that represents the asynchronous operation. The task result contains the entity if found, or null if no entity with the given key is found.
    /// </returns>
    public Task<TEntity?> GetByKeyAsync<TKey>(TKey key);

    /// <summary>
    /// Asynchronously retrieves a single record based on the provided filter.
    /// </summary>
    /// <param name="filter">The expression to filter the entity.</param>
    /// <returns>A task that represents the asynchronous operation, containing the entity if found, or null.</returns>
    public Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter);

    /// <summary>
    /// Asynchronously retrieves all records that match the provided filter.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a collection of matching entities.</returns>
    public Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Asynchronously retrieves an IQueryable to allow further query operations on the entities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing an IQueryable of the entities.</returns>
    public Task<IQueryable<TEntity>> GetQueryAsync();

    /// <summary>
    /// Asynchronously adds a new entity to the context.
    /// </summary>
    /// <param name="entry">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddAsync(TEntity entry);

    /// <summary>
    /// Asynchronously adds multiple new entities to the context.
    /// </summary>
    /// <param name="entries">The collection of entities to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddManyAsync(IEnumerable<TEntity> entries);

    /// <summary>
    /// Asynchronously updates an existing entity in the context.
    /// </summary>
    /// <param name="entry">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UpdateAsync(TEntity entry);

    /// <summary>
    /// Asynchronously removes an entity from the context based on its primary key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identify the entity.</typeparam>
    /// <param name="key">The key of the entity to be removed.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. The task will complete when the entity has been removed.
    /// </returns>
    public Task RemoveByKeyAsync<TKey>(TKey key);

    /// <summary>
    /// Asynchronously removes an existing entity from the context.
    /// </summary>
    /// <param name="entry">The entity to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RemoveAsync(TEntity entry);

    /// <summary>
    /// Asynchronously removes multiple entities from the context.
    /// </summary>
    /// <param name="entries">The collection of entities to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RemoveManyAsync(IEnumerable<TEntity> entries);
}