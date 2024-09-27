using Pavas.Patterns.Context.Contracts;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Runtime.TenantContext;

namespace UnitOfWork.Example;

public class UnitOfWorkConfigurator : IUnitOfWorkConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        var tenantContext = serviceProvider.GetRequiredService<IContextProvider<TenantContext>>();

        databaseOptions.ConnectionString = "Host=localhost;Database=UnitOfWork;Username=root;Password=root";
        databaseOptions.TenantId = tenantContext.Context!.TenantId;
        databaseOptions.EnsureCreated = true;
        databaseOptions.SoftDelete = true;
        return databaseOptions;
    }
}