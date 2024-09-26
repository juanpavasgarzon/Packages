using Microsoft.EntityFrameworkCore.Storage;
using Pavas.Patterns.UnitOfWork.Abstracts;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.Extensions;
using Pavas.Patterns.UnitOfWork.Options;

namespace Pavas.Patterns.UnitOfWork;

internal class UnitOfWork(DatabaseContext context, IServiceProvider provider) : IUnitOfWork
{
    /// <summary>
    /// Asynchronously retrieves a repository for a specific entity type.
    /// This is the default repository retrieval method and does not apply any custom configuration options.
    /// The repository does not track changes made to the entity instances.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete. Defaults to a new token if not provided.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type.</returns>
    public Task<IRepository<TEntity>> GetRepositoryAsync<TEntity>(CancellationToken token = new())
        where TEntity : class
    {
        return BaseGetRepositoryAsync<TEntity>(new RepositoryOptions(), token);
    }

    /// <summary>
    /// Asynchronously retrieves a repository for a specific entity type with custom configuration.
    /// The repository is configured based on the provided <see cref="IRepositoryOptions"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <param name="configure">An <see cref="Action{IRepositoryOptions}"/> delegate used to configure the repository options.</param>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured with the provided options.</returns>
    public Task<IRepository<TEntity>> GetRepositoryAsync<TEntity>(Action<IRepositoryOptions> configure,
        CancellationToken token = new()) where TEntity : class
    {
        var options = new RepositoryOptions();
        configure.Invoke(options);
        return BaseGetRepositoryAsync<TEntity>(options, token);
    }

    /// <summary>
    /// Asynchronously retrieves a repository for a specific entity type, using a configurator of type <typeparamref name="TConfigurator"/>.
    /// The configurator is responsible for configuring the repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <typeparam name="TConfigurator">The type of the configurator used to configure the repository. Must implement <see cref="IRepositoryConfigurator"/> and have a parameterless constructor.</typeparam>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured by the specified configurator.</returns>
    public Task<IRepository<TEntity>> GetRepositoryAsync<TEntity, TConfigurator>(CancellationToken token = new())
        where TEntity : class where TConfigurator : class, IRepositoryConfigurator, new()
    {
        var configurator = new TConfigurator();
        var options = configurator.Configure(provider, new RepositoryOptions());
        return BaseGetRepositoryAsync<TEntity>(options, token);
    }

    /// <summary>
    /// Asynchronously retrieves a repository for a specific entity type based on the provided repository options.
    /// This method handles tenant ID and correlation ID configurations before returning the repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which the repository is being retrieved. Must be a class.</typeparam>
    /// <param name="options">The options used to configure the repository, including tenant ID and correlation ID.</param>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IRepository{TEntity}"/> for the specified entity type, configured with the provided options.</returns>
    private Task<IRepository<TEntity>> BaseGetRepositoryAsync<TEntity>(IRepositoryOptions options,
        CancellationToken token = new()) where TEntity : class
    {
        if (!options.TenantId.IsNullOrEmptyOrWhiteSpace())
            context.SetTenantId(options.TenantId);

        if (!options.CorrelationId.IsNullOrEmptyOrWhiteSpace())
            context.SetCorrelationId(options.TenantId);

        var task = new Task<IRepository<TEntity>>(() => new Repository<TEntity>(context, token), token);
        task.Start();
        return task;
    }

    /// <summary>
    /// Begins a database transaction asynchronously.
    /// </summary>
    /// <param name="token">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation, containing the database transaction.</returns>
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = new())
    {
        return context.Database.BeginTransactionAsync(token);
    }

    /// <summary>
    /// Saves all changes made in the current context asynchronously.
    /// </summary>
    /// <param name="token">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation, containing the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken token = new())
    {
        return context.SaveChangesAsync(token);
    }
}