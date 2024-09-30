using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.Contracts.Options;

namespace UnitOfWork.Example;

public class DatabaseConfigurator : IDatabaseConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        databaseOptions.ConnectionString = "Host=localhost;Database=MyDatabase;Username=root;Password=root";
        databaseOptions.TenantId = "Default";
        databaseOptions.CorrelationId = Guid.NewGuid().ToString();
        databaseOptions.EnsureCreated = true;
        return databaseOptions;
    }
}