using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context.DependencyInjection;

public static class Extensions
{
    public static void AddContext<TContext>(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime
    ) where TContext : class
    {
        var factoryDescriptor = new Descriptor<IContextFactory<TContext>, ContextFactory<TContext>>(serviceLifetime);
        serviceCollection.Add(factoryDescriptor);

        var providerDescriptor = new Descriptor<IContextFactory<TContext>, ContextFactory<TContext>>(serviceLifetime);
        serviceCollection.Add(providerDescriptor);
    }

    public static void AddContext<TContext>(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime,
        Func<IServiceProvider, TContext> initializer
    ) where TContext : class
    {
        var factoryDescriptor = new Descriptor<IContextFactory<TContext>, ContextFactory<TContext>>(serviceLifetime);
        serviceCollection.Add(factoryDescriptor);

        var providerDescriptor = new Descriptor<IContextFactory<TContext>, ContextFactory<TContext>>(serviceLifetime);
        serviceCollection.Add(providerDescriptor);

        serviceCollection.AddHostedService<ContextHostedService<TContext>>(services =>
        {
            var context = initializer.Invoke(services);
            var factory = services.GetRequiredService<IContextFactory<TContext>>();
            return new ContextHostedService<TContext>(factory, context);
        });
    }
}