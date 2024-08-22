using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context.DependencyInjection;

public static class Extensions
{
    private static void BaseAddContextLifetime<TContext>(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime
    ) where TContext : class
    {
        var factoryDescriptor = new ServiceDescriptor(
            typeof(IContextFactory<TContext>),
            typeof(AsyncLocalContextFactory<TContext>),
            serviceLifetime
        );

        services.Add(factoryDescriptor);

        var providerDescriptor = new ServiceDescriptor(
            typeof(IContextProvider<TContext>),
            typeof(AsyncLocalContextProvider<TContext>),
            serviceLifetime
        );

        services.Add(providerDescriptor);
    }

    private static void BaseAddGlobalContext<TContext>(
        this IServiceCollection services,
        TContext context
    ) where TContext : class
    {
        services.AddSingleton(context);
        services.AddSingleton<IContextProvider<TContext>, GlobalContextProvider<TContext>>(_ =>
        {
            var contextProvider = GlobalContextProvider<TContext>.GetInstance();
            contextProvider.Context = context;
            return contextProvider;
        });
    }

    public static void AddScopedContext<TContext>(this IServiceCollection services) where TContext : class
    {
        services.BaseAddContextLifetime<TContext>(ServiceLifetime.Scoped);
    }

    public static void AddTransientContext<TContext>(this IServiceCollection services) where TContext : class
    {
        services.BaseAddContextLifetime<TContext>(ServiceLifetime.Transient);
    }

    public static void AddSingletonContext<TContext>(
        this IServiceCollection services,
        Func<IServiceProvider, TContext> initializer
    ) where TContext : class
    {
        var serviceProviderFactory = new DefaultServiceProviderFactory();
        var serviceProvider = serviceProviderFactory.CreateServiceProvider(services);
        var context = initializer(serviceProvider);
        services.BaseAddGlobalContext(context);
    }

    public static void AddSingletonContext<TContext>(
        this IServiceCollection services,
        TContext context
    ) where TContext : class
    {
        services.BaseAddGlobalContext(context);
    }
}