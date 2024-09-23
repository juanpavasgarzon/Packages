using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Options.Extensions;

internal static class DatabaseOptionsBuilderExtensions
{
    public static void UseDatabaseOptions(this DbContextOptionsBuilder optionsBuilder, IDatabaseOptions databaseOptions)
    {
        var extension = new DatabaseOptionsExtension(databaseOptions);
        (optionsBuilder as IDbContextOptionsBuilderInfrastructure).AddOrUpdateExtension(extension);
    }
}