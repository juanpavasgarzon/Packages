using Microsoft.EntityFrameworkCore;
using Pavas.Patterns.UnitOfWork.Abstracts;

namespace UnitOfWork.Example;

internal class UnitOfWorkContext(DbContextOptions contextOptions) : DatabaseContext(contextOptions)
{
    protected override void GetProvider(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        var version = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, version, builder => builder.EnableRetryOnFailure(3));
    }
}