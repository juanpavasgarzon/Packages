using Microsoft.EntityFrameworkCore.Storage;

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
    /// <typeparam name="TConfigurator">The type of the configurator used to configure the repository. Must implement <see cref="IRepositoryConfigurator"/> and have a parameterless constructor.</typeparam>
    /// <returns>The result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured by the specified configurator.</returns>
    public IRepository<TEntity> GetRepository<TEntity, TConfigurator>() where TEntity : class
        where TConfigurator : class, IRepositoryConfigurator, new();
    
    /// <summary>
    /// Asynchronously retrieves a repository for a specific entity type.
    /// This is the default repository retrieval method and does not apply any custom configuration options.
    /// The repository does not track changes made to the entity instances.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete. Defaults to a new token if not provided.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type.</returns>
    public Task<IRepository<TEntity>> GetRepositoryAsync<TEntity>(CancellationToken token = new())
        where TEntity : class;
    
    /// <summary>
    /// Asynchronously retrieves a repository for a specific entity type with custom configuration.
    /// The repository is configured based on the provided <see cref="IRepositoryOptions"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <param name="configure">An <see cref="Action{IRepositoryOptions}"/> delegate used to configure the repository options.</param>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured with the provided options.</returns>
    public Task<IRepository<TEntity>> GetRepositoryAsync<TEntity>(Action<IRepositoryOptions> configure,
        CancellationToken token = new()) where TEntity : class;

    /// <summary>
    /// Asynchronously retrieves a repository for a specific entity type, using a configurator of type <typeparamref name="TConfigurator"/>.
    /// The configurator is responsible for configuring the repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <typeparam name="TConfigurator">The type of the configurator used to configure the repository. Must implement <see cref="IRepositoryConfigurator"/> and have a parameterless constructor.</typeparam>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured by the specified configurator.</returns>
    public Task<IRepository<TEntity>> GetRepositoryAsync<TEntity, TConfigurator>(CancellationToken token = new())
        where TEntity : class where TConfigurator : class, IRepositoryConfigurator, new();

    /// <summary>
    /// Asynchronously begins a new database transaction.
    /// This transaction allows multiple operations to be executed atomically, ensuring that all operations
    /// are either completed successfully or rolled back in case of an error.
    /// </summary>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the transaction to begin.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IDbContextTransaction"/> 
    /// that can be used to manage the transaction.</returns>
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = new());

    /// <summary>
    /// Asynchronously saves all changes made in the current context to the database.
    /// This method ensures that any modifications to tracked entities are persisted.
    /// </summary>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the save operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken token = new());
}