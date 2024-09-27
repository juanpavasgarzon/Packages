using Pavas.Patterns.Context.Contracts;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Runtime.TenantContext;

namespace UnitOfWork.Example;

public class UnitOfWorkConfigurator : IUnitOfWorkConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        var tenantContext = serviceProvider.GetRequiredService<IContextProvider<TenantContext>>();
        var tenant = tenantContext.Context!;

        databaseOptions.ConnectionString = tenant.Connection;
        databaseOptions.TenantId = tenant.TenantId;
        databaseOptions.EnsureCreated = true;
        databaseOptions.SoftDelete = true;
        return databaseOptions;
    }
}