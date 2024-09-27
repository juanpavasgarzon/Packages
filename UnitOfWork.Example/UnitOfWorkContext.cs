using Microsoft.EntityFrameworkCore;
using Pavas.Patterns.UnitOfWork.Abstracts;

namespace UnitOfWork.Example;

internal sealed class UnitOfWorkContext(ILogger<UnitOfWorkContext> logger, DbContextOptions contextOptions)
    : DatabaseContext(contextOptions)
{
    public required DbSet<MyEntity> MyEntities { get; set; }

    protected override void GetProvider(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        var version = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, version, builder => builder.EnableRetryOnFailure(3));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(message => logger.LogInformation("{Message}", message), LogLevel.Information);
        base.OnConfiguring(optionsBuilder);
    }
}