using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class UnitOfWorkConfigurator : IUnitOfWorkConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        databaseOptions.ConnectionString = "Host=localhost;Database=UnitOfWork;Username=root;Password=root";
        databaseOptions.TenantId = "App";
        databaseOptions.EnsureCreated = true;
        databaseOptions.SoftDelete = true;
        return databaseOptions;
    }
}