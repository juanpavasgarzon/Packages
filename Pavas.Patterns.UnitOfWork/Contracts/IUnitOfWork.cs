using Microsoft.EntityFrameworkCore.Storage;

namespace Pavas.Patterns.UnitOfWork.Contracts;

public interface IUnitOfWork
{
    /// <summary>
    /// Retrieves the repository for a specific entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which the repository is requested.</typeparam>
    /// <param name="token">A cancellation token to cancel the operation if needed.</param>
    /// <returns>An instance of the repository for the specified entity.</returns>
    public IRepository<TEntity> GetRepository<TEntity>(CancellationToken token = new()) where TEntity : class, IEntity;

    /// <summary>
    /// Begins a database transaction asynchronously.
    /// </summary>
    /// <param name="token">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation, containing the database transaction.</returns>
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = new());

    /// <summary>
    /// Saves all changes made in the current context asynchronously.
    /// </summary>
    /// <param name="token">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation, containing the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken token = new());
}