using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Options;

internal class DatabaseOptions : IDatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public bool SoftDelete { get; set; } = false;
    public bool EnsureCreated { get; set; } = false;

    public override string ToString()
    {
        return $"Connection={ConnectionString};Tenant={TenantId};SoftDelete={SoftDelete};EnsureCreated={EnsureCreated};";
    }
}