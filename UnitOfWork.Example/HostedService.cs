using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class HostedService(IUnitOfWork unitOfWork) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var repository = unitOfWork.GetRepository<MyEntity>();
        var all = await repository.GetAllAsync(stoppingToken);

        foreach (var one in all)
        {
            await repository.RemoveAsync(one, stoppingToken);
        }

        await unitOfWork.SaveChangesAsync(stoppingToken);

    }
}