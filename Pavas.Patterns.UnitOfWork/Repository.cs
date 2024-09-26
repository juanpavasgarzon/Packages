using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork;

internal class Repository<TEntity>(DbContext context, CancellationToken token = new())
    : IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Asynchronously retrieves a single record by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the entity if found, or null.</returns>
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync([id], token);
    }

    /// <summary>
    /// Asynchronously retrieves a single record based on the provided filter.
    /// </summary>
    /// <param name="filter">The expression to filter the entity.</param>
    /// <returns>A task that represents the asynchronous operation, containing the entity if found, or null.</returns>
    public Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter)
    {
        return context.Set<TEntity>().Where(filter).FirstOrDefaultAsync(token);
    }

    /// <summary>
    /// Asynchronously retrieves all records that match the provided filter.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a collection of matching entities.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync(token);
    }

    /// <summary>
    /// Asynchronously retrieves an IQueryable to allow further query operations on the entities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing an IQueryable of the entities.</returns>
    public async Task<IQueryable<TEntity>> GetQueryAsync()
    {
        return await Task.Run(() => context.Set<TEntity>().AsQueryable(), token);
    }

    /// <summary>
    /// Asynchronously adds a new entity to the context.
    /// </summary>
    /// <param name="entry">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddAsync(TEntity entry)
    {
        await context.Set<TEntity>().AddAsync(entry, token);
    }

    /// <summary>
    /// Asynchronously adds multiple new entities to the context.
    /// </summary>
    /// <param name="entries">The collection of entities to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddManyAsync(IEnumerable<TEntity> entries)
    {
        await context.Set<TEntity>().AddRangeAsync(entries, token);
    }

    /// <summary>
    /// Asynchronously updates an existing entity in the context.
    /// </summary>
    /// <param name="entry">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task UpdateAsync(TEntity entry)
    {
        await Task.Run(() => context.Set<TEntity>().Update(entry), token);
    }

    /// <summary>
    /// Asynchronously removes an existing entity from the context.
    /// </summary>
    /// <param name="entry">The entity to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RemoveAsync(TEntity entry)
    {
        await Task.Run(() => context.Set<TEntity>().Remove(entry), token);
    }

    /// <summary>
    /// Asynchronously removes multiple entities from the context.
    /// </summary>
    /// <param name="entries">The collection of entities to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RemoveManyAsync(IEnumerable<TEntity> entries)
    {
        await Task.Run(() => context.Set<TEntity>().RemoveRange(entries), token);
    }
}