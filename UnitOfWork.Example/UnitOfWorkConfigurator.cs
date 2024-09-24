using Pavas.Patterns.Context.Contracts;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class UnitOfWorkConfigurator : IUnitOfWorkConfigurator
{
    public IDatabaseOptions Configure(IServiceProvider serviceProvider, IDatabaseOptions databaseOptions)
    {
        var provider = serviceProvider.GetRequiredService<IContextProvider<TenantContext>>();
        databaseOptions.ConnectionString = "Host=localhost;Database=UnitOfWork;Username=root;Password=root";
        databaseOptions.TenantId = provider.Context!.TenantId;
        databaseOptions.EnsureCreated = true;
        databaseOptions.SoftDelete = true;
        return databaseOptions;
    }
}