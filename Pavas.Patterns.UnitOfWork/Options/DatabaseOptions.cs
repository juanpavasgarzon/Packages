using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Options;

internal class DatabaseOptions : IDatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DefaultTenant { get; set; } = "Default";
    public bool SoftDelete { get; set; } = false;
    public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Scoped;

    public override string ToString()
    {
        return $"Connection={ConnectionString};Tenant={DefaultTenant};SoftDelete={SoftDelete}";
    }
}