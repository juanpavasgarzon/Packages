using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context.DependencyInjection;

/// <summary>
/// Provides extension methods for adding context management services to the dependency injection container.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Registers the context factory and provider with a specified service lifetime.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to register.</typeparam>
    /// <param name="services">The service collection to add the context services to.</param>
    /// <param name="serviceLifetime">The lifetime of the context services (Scoped, Transient, or Singleton).</param>
    private static void BaseAddContextLifetime<TContext>(this IServiceCollection services,
        ServiceLifetime serviceLifetime) where TContext : class
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

    /// <summary>
    /// Registers a global context with singleton lifetime and a specific instance of the context.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to register.</typeparam>
    /// <param name="services">The service collection to add the context services to.</param>
    /// <param name="context">The instance of the context to register.</param>
    private static void BaseAddGlobalContext<TContext>(this IServiceCollection services, TContext context)
        where TContext : class
    {
        services.AddSingleton(context);
        services.AddSingleton<IContextProvider<TContext>, GlobalContextProvider<TContext>>(_ =>
        {
            var contextProvider = GlobalContextProvider<TContext>.GetInstance();
            contextProvider.Context = context;
            return contextProvider;
        });
    }

    /// <summary>
    /// Registers the context services with scoped lifetime.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to register.</typeparam>
    /// <param name="services">The service collection to add the context services to.</param>
    public static void AddScopedContext<TContext>(this IServiceCollection services) where TContext : class
    {
        services.BaseAddContextLifetime<TContext>(ServiceLifetime.Scoped);
    }

    /// <summary>
    /// Registers the context services with transient lifetime.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to register.</typeparam>
    /// <param name="services">The service collection to add the context services to.</param>
    public static void AddTransientContext<TContext>(this IServiceCollection services) where TContext : class
    {
        services.BaseAddContextLifetime<TContext>(ServiceLifetime.Transient);
    }

    /// <summary>
    /// Registers the context services with singleton lifetime using a factory method for initialization.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to register.</typeparam>
    /// <param name="services">The service collection to add the context services to.</param>
    /// <param name="initializer">The factory method used to initialize the context.</param>
    public static void AddSingletonContext<TContext>(this IServiceCollection services,
        Func<IServiceProvider, TContext> initializer) where TContext : class
    {
        var serviceProviderFactory = new DefaultServiceProviderFactory();
        var serviceProvider = serviceProviderFactory.CreateServiceProvider(services);
        var context = initializer(serviceProvider);
        services.BaseAddGlobalContext(context);
    }

    /// <summary>
    /// Registers the context services with singleton lifetime using a specific instance of the context.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to register.</typeparam>
    /// <param name="services">The service collection to add the context services to.</param>
    /// <param name="context">The instance of the context to register.</param>
    public static void AddSingletonContext<TContext>(this IServiceCollection services, TContext context)
        where TContext : class
    {
        services.BaseAddGlobalContext(context);
    }
}