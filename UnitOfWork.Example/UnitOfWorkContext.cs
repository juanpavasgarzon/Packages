using Microsoft.EntityFrameworkCore;
using Pavas.Patterns.UnitOfWork.Abstracts;

namespace UnitOfWork.Example;

internal sealed class UnitOfWorkContext(DbContextOptions contextOptions) : DatabaseContext(contextOptions)
{
    public required DbSet<MyEntity> MyEntities { get; set; }

    protected override void GetProvider(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        var version = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, version, builder => builder.EnableRetryOnFailure(3));
    }
}