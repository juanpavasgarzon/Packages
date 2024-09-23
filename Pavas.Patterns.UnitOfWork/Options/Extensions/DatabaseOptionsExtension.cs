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
        if (string.IsNullOrEmpty(_databaseOptions.ConnectionString))
            throw new InvalidOperationException("ConnectionString is required.");

        if (string.IsNullOrWhiteSpace(_databaseOptions.DefaultTenant))
            throw new InvalidOperationException("DefaultTenant is required.");
    }

    public IDatabaseOptions GetDatabaseOptions()
    {
        return _databaseOptions;
    }
}