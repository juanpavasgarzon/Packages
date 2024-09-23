using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Options.Extensions;

internal class DatabaseOptionsExtension : IDbContextOptionsExtension
{
    private readonly IDatabaseOptions _databaseOptions;

    public DatabaseOptionsExtension(IDatabaseOptions options)
    {
        _databaseOptions = options;
        Info = new ExtensionInfo(this);
    }

    public DbContextOptionsExtensionInfo Info { get; }

    public void ApplyServices(IServiceCollection services)
    {
    }

    public void Validate(IDbContextOptions options)
    {
        var connectionString = _databaseOptions.ConnectionString;
        if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrWhiteSpace(connectionString))
            return;

        throw new InvalidOperationException("ConnectionString is required.");
    }

    public IDatabaseOptions GetDatabaseOptions()
    {
        return _databaseOptions;
    }
}