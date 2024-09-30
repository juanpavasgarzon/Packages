using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class HostedService(IUnitOfWork unitOfWork) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var repository = unitOfWork.GetRepository<MyEntity>();
            await repository.AddAsync(new MyEntity());
            await unitOfWork.SaveChangesAsync(stoppingToken);
        }
    }
}