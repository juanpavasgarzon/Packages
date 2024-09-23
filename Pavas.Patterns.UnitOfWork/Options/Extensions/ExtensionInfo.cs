using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Pavas.Patterns.UnitOfWork.Options.Extensions;

internal class ExtensionInfo(DatabaseOptionsExtension extension) : DbContextOptionsExtensionInfo(extension)
{
    private readonly DatabaseOptionsExtension _extension = extension;

    public override string LogFragment => "Using DatabaseOptionsExtension";

    public override bool IsDatabaseProvider => false;

    public override int GetServiceProviderHashCode() => _extension.GetHashCode();

    public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
    {
        debugInfo["DatabaseOptions:ConnectionString"] = _extension.GetDatabaseOptions().ConnectionString;
        debugInfo["DatabaseOptions:SoftDelete"] = _extension.GetDatabaseOptions().SoftDelete.ToString();
        debugInfo["DatabaseOptions:Tenant"] = _extension.GetDatabaseOptions().TenantId;
    }

    public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
    {
        if (other is not ExtensionInfo otherExtension)
            return false;

        var otherToString = otherExtension._extension.GetDatabaseOptions().ToString();
        return _extension.GetDatabaseOptions().ToString() == otherToString;
    }
}