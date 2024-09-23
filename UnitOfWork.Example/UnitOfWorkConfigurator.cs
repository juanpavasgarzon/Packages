using Pavas.Patterns.Context.Contracts;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class UnitOfWorkConfigurator : IUnitOfWorkConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        var provider = serviceProvider.GetRequiredService<IContextProvider<TenantContext>>();

        databaseOptions.TenantId = provider.Context!.TenantId;
        return databaseOptions;
    }
}