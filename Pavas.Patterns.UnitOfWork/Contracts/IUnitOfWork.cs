using Microsoft.EntityFrameworkCore.Storage;
using Pavas.Patterns.UnitOfWork.Abstracts;
using Pavas.Patterns.UnitOfWork.Contracts.Options;

namespace Pavas.Patterns.UnitOfWork.Contracts;

public interface IUnitOfWork
{
    /// <summary>
    /// Retrieves a repository for a specific entity type.
    /// This is the default repository retrieval method and does not apply any custom configuration options.
    /// The repository does not track changes made to the entity instances.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <returns>The result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type.</returns>
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// Retrieves a repository for a specific entity type with custom configuration.
    /// The repository is configured based on the provided <see cref="IRepositoryOptions"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <param name="configure">An <see cref="Action{IRepositoryOptions}"/> delegate used to configure the repository options.</param>
    /// <returns>The result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured with the provided options.</returns>
    public IRepository<TEntity> GetRepository<TEntity>(Action<IRepositoryOptions> configure)
        where TEntity : class;

    /// <summary>
    /// Retrieves a repository for a specific entity type, using a configurator of type <typeparamref name="TConfigurator"/>.
    /// The configurator is responsible for configuring the repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <typeparam name="TConfigurator">
    /// The type of the configurator used to configure the repository.
    /// Must implement <see cref="IRepositoryConfigurator"/> and have a parameterless constructor.
    /// </typeparam>
    /// <returns>The result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured by the specified configurator.</returns>
    public IRepository<TEntity> GetRepository<TEntity, TConfigurator>() where TEntity : class
        where TConfigurator : class, IRepositoryConfigurator, new();

    /// <summary>
    /// Executes the provided operation within a database transaction using the configured execution strategy for handling transient failures.
    /// </summary>
    /// <param name="operation">A delegate that represents the operation to execute. The transaction is passed as a parameter to the delegate.</param>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// This method ensures that the provided operation, which includes database transactions, is retried if transient failures occur.
    /// It uses the execution strategy configured in the <see cref="DatabaseContext"/> to handle retries.
    /// The operation is responsible for performing database operations inside the provided transaction and ensuring that the transaction is committed or rolled back as necessary.
    /// </remarks>
    public Task ExecutionStrategyAsync(Func<IDbContextTransaction, Task> operation, CancellationToken token = new());

    /// <summary>
    /// Executes the provided operation within a database transaction using the configured execution strategy for handling transient failures.
    /// </summary>
    /// <param name="operation">A delegate that represents the operation to execute. The transaction is passed as a parameter to the delegate.</param>
    /// <returns>A task that represents the transaction operation.</returns>
    /// <remarks>
    /// This method ensures that the provided operation, which includes database transactions, is retried if transient failures occur.
    /// It uses the execution strategy configured in the <see cref="DatabaseContext"/> to handle retries.
    /// The operation is responsible for performing database operations inside the provided transaction and ensuring that the transaction is committed or rolled back as necessary.
    /// </remarks>
    public void ExecutionStrategy(Action<IDbContextTransaction> operation);

    /// <summary>
    /// Asynchronously saves all changes made in the current context to the database.
    /// This method ensures that any modifications to tracked entities are persisted.
    /// </summary>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the save operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken token = new());

    /// <summary>
    /// Saves all changes made in the current context to the database.
    /// This method ensures that any modifications to tracked entities are persisted.
    /// </summary>
    /// <returns>The result contains the number of state entries written to the database.</returns>
    public int SaveChanges();

    /// <summary>
    /// Retrieves the current instance of the <see cref="DatabaseContext"/>.
    /// </summary>
    /// <returns>The current instance of <see cref="DatabaseContext"/> being used.</returns>
    /// <remarks>
    /// This method provides access to the underlying database context, allowing operations such as querying, 
    /// attaching entities, or other direct interactions with the Entity Framework context. Use this method with caution 
    /// to avoid bypassing any unit of work patterns or repository abstractions that manage context lifecycle and transaction handling.
    /// </remarks>
    public DatabaseContext GetContext();
}