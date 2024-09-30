using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Runtime.TenantContext;

namespace UnitOfWork.Example;

public class UnitOfWorkConfigurator : IUnitOfWorkConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        var tenantContext = serviceProvider.GetRequiredService<List<Tenant>>();

        databaseOptions.ConnectionString = tenantContext[0].Connection;
        databaseOptions.TenantId = tenantContext[0].Id;
        databaseOptions.EnsureCreated = true;
        return databaseOptions;
    }
}