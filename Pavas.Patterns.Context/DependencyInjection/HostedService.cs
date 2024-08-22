using Microsoft.Extensions.Hosting;
using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context.DependencyInjection;

public sealed class ContextHostedService<TContext>(
    IContextFactory<TContext> contextFactory,
    TContext context
) : IHostedService where TContext : class
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        contextFactory.Construct(context);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        contextFactory.Destruct();
        return Task.CompletedTask;
    }
}