using Microsoft.Extensions.Hosting;
using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context.DependencyInjection;

internal sealed class ContextHostedService<TContext>(
    IContextFactory<TContext> contextFactory,
    TContext initialContext
) : IHostedService where TContext : class
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        contextFactory.Construct(initialContext);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        contextFactory.Destruct();
        return Task.CompletedTask;
    }
}