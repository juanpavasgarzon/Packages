namespace Pavas.Patterns.UnitOfWork.Contracts;

/// <summary>
/// Defines the contract for configuring repository options. 
/// Implementations of this interface are responsible for setting up custom options for repositories, 
/// such as tenant and correlation information.
/// </summary>
public interface IRepositoryConfigurator
{
    /// <summary>
    /// Configures the repository options, such as tenant ID and correlation ID, for a specific service.
    /// </summary>
    /// <param name="serviceProvider">The service provider that can be used to resolve dependencies for configuration.</param>
    /// <param name="repositoryOptions">The repository options that will be configured, including tenant and correlation information.</param>
    /// <returns>The configured <see cref="IRepositoryOptions"/> instance with the appropriate settings.</returns>
    public IRepositoryOptions Configure(IServiceProvider serviceProvider, IRepositoryOptions repositoryOptions);
}