using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class HostedService(IUnitOfWork unitOfWork) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await unitOfWork.ExecutionStrategyAsync(async transaction =>
        {
            var repository = unitOfWork.GetRepository<MyEntity>();
            try
            {
                await repository.AddAsync(new MyEntity(), stoppingToken);
                await repository.AddAsync(new MyEntity(), stoppingToken);
                await repository.AddAsync(new MyEntity(), stoppingToken);
                await repository.AddAsync(new MyEntity(), stoppingToken);
                await unitOfWork.SaveChangesAsync(stoppingToken);

                await repository.AddAsync(new MyEntity(), stoppingToken);
                await repository.AddAsync(new MyEntity(), stoppingToken);
                await repository.AddAsync(new MyEntity(), stoppingToken);
                await repository.AddAsync(new MyEntity(), stoppingToken);

                await unitOfWork.SaveChangesAsync(stoppingToken);
                await transaction.CommitAsync(stoppingToken);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(stoppingToken);
                Console.WriteLine(e);
            }
        }, stoppingToken);
    }
}