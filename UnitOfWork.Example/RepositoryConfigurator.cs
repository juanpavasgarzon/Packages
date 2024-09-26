using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class RepositoryConfigurator : IRepositoryConfigurator
{
    public IRepositoryOptions Configure(IServiceProvider serviceProvider, IRepositoryOptions repositoryOptions)
    {
        repositoryOptions.CorrelationId = Guid.NewGuid().ToString();
        repositoryOptions.TenantId = "App";
        return repositoryOptions;
    }
}