using Pavas.Patterns.UnitOfWork.Contracts.Options;

namespace Pavas.Patterns.UnitOfWork.Contracts;

/// <summary>
/// Defines a configurator interface for configuring the UnitOfWork and DbContext within a dependency injection container.
/// </summary>
public interface IDatabaseConfigurator
{
    /// <summary>
    /// Configures the database options and other necessary services using the provided service provider.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve services required for configuration.</param>
    /// <param name="databaseOptions">The <see cref="IDatabaseOptions"/> used to configure options from database.</param>
    /// <returns>Returns the <see cref="IDatabaseOptions"/> that contains the configuration options for the database.</returns>
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions);
}