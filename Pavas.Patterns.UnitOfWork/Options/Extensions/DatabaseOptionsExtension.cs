using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.UnitOfWork.Contracts.Options;
using Pavas.Patterns.UnitOfWork.Exceptions;
using Pavas.Patterns.UnitOfWork.Extensions;

namespace Pavas.Patterns.UnitOfWork.Options.Extensions;

/// <summary>
/// Represents a custom extension for configuring Entity Framework Core options using <see cref="IDatabaseOptions"/>.
/// This extension allows specific database options such as connection string and soft delete functionality to be applied to the EF Core context.
/// </summary>
internal class DatabaseOptionsExtension : IDbContextOptionsExtension
{
    private readonly IDatabaseOptions _databaseOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseOptionsExtension"/> class.
    /// </summary>
    /// <param name="options">The <see cref="IDatabaseOptions"/> that provides custom database configuration options.</param>
    public DatabaseOptionsExtension(IDatabaseOptions options)
    {
        _databaseOptions = options;
        Info = new ExtensionInfo(this);
    }

    /// <summary>
    /// Gets information about the extension, including debug information and service provider requirements.
    /// </summary>
    public DbContextOptionsExtensionInfo Info { get; }

    /// <summary>
    /// Applies custom services to the EF Core context. Currently, this method is not used but is available for future service configurations.
    /// </summary>
    /// <param name="services">The service collection to which custom services can be added.</param>
    public void ApplyServices(IServiceCollection services)
    {
    }

    /// <summary>
    /// Validates the database options, ensuring that a valid connection string is provided.
    /// Throws an <see cref="InvalidOperationException"/> if the connection string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="options">The context options to validate.</param>
    /// <exception cref="InvalidOperationException">Thrown if the connection string is not provided.</exception>
    public void Validate(IDbContextOptions options)
    {
        if (_databaseOptions.ConnectionString.IsNullOrEmptyOrWhiteSpace())
        {
            throw new RequireMemberException("ConnectionString is required.");
        }
    }

    /// <summary>
    /// Retrieves the custom database options configured for this extension.
    /// </summary>
    /// <returns>The <see cref="IDatabaseOptions"/> instance containing the custom options.</returns>
    public IDatabaseOptions GetDatabaseOptions()
    {
        return _databaseOptions;
    }
}