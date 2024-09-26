using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Options.Extensions;

/// <summary>
/// Provides extension methods for configuring the <see cref="DbContextOptionsBuilder"/> with custom database options.
/// </summary>
internal static class DatabaseOptionsBuilderExtensions
{
    /// <summary>
    /// Configures the <see cref="DbContextOptionsBuilder"/> to use the provided <see cref="IDatabaseOptions"/>.
    /// This method adds or updates a custom database options extension within the EF Core context configuration.
    /// </summary>
    /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/> instance being configured.</param>
    /// <param name="databaseOptions">The custom database options to be used for configuring the context.</param>
    public static void UseDatabaseOptions(this DbContextOptionsBuilder optionsBuilder, IDatabaseOptions databaseOptions)
    {
        var extension = new DatabaseOptionsExtension(databaseOptions);
        (optionsBuilder as IDbContextOptionsBuilderInfrastructure).AddOrUpdateExtension(extension);
    }
}