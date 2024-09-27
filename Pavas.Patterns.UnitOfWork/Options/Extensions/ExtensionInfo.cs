using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Pavas.Patterns.UnitOfWork.Options.Extensions;

/// <summary>
/// Provides detailed information about the <see cref="DatabaseOptionsExtension"/>, including debug info, service provider configuration, and logging details.
/// </summary>
/// <param name="extension">The <see cref="DatabaseOptionsExtension"/> that this instance describes.</param>
internal class ExtensionInfo(DatabaseOptionsExtension extension) : DbContextOptionsExtensionInfo(extension)
{
    private readonly DatabaseOptionsExtension _extension = extension;

    /// <summary>
    /// Gets a log fragment that describes the extension, useful for debugging purposes.
    /// </summary>
    public override string LogFragment => "Using DatabaseOptionsExtension";

    /// <summary>
    /// Indicates whether this extension provides a database provider. This extension does not provide a database provider.
    /// </summary>
    public override bool IsDatabaseProvider => false;

    /// <summary>
    /// Returns a hash code for the current extension based on the configured options, used to compare service providers.
    /// </summary>
    /// <returns>The hash code of the current extension.</returns>
    public override int GetServiceProviderHashCode() => _extension.GetHashCode();

    /// <summary>
    /// Populates the provided dictionary with debug information about the current database options, such as connection string and soft delete setting.
    /// </summary>
    /// <param name="debugInfo">A dictionary that will be populated with debug information about the extension.</param>
    public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
    {
        debugInfo["DatabaseOptions:ConnectionString"] = _extension.GetDatabaseOptions().ConnectionString;
        debugInfo["DatabaseOptions:TenantId"] = _extension.GetDatabaseOptions().TenantId;
        debugInfo["DatabaseOptions:CorrelationId"] = _extension.GetDatabaseOptions().CorrelationId;
        debugInfo["DatabaseOptions:EnsureCreated"] = _extension.GetDatabaseOptions().EnsureCreated.ToString();
    }

    /// <summary>
    /// Determines whether two service providers should use the same instance based on the configured database options.
    /// </summary>
    /// <param name="other">The other <see cref="DbContextOptionsExtensionInfo"/> instance to compare against.</param>
    /// <returns><see langword="true"/> if both extensions have identical database options; otherwise, <see langword="false"/>.</returns>
    public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
    {
        if (other is not ExtensionInfo otherExtension)
            return false;

        var otherToString = otherExtension._extension.GetDatabaseOptions().ToString();
        return _extension.GetDatabaseOptions().ToString() == otherToString;
    }
}