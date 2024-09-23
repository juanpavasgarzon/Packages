using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.UnitOfWork.Abstracts;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.Options;
using Pavas.Patterns.UnitOfWork.Options.Extensions;

namespace Pavas.Patterns.UnitOfWork.DependencyInjection;

/// <summary>
/// Provides extension methods to add UnitOfWork and related services to the DI container.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Registers the UnitOfWork pattern and DbContext in the dependency injection container with options configuration.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the UnitOfWork and DbContext will be added.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="IDatabaseOptions"/>.</param>
    public static void AddUnitOfWork<TContext>(this IServiceCollection services,
        Action<IDatabaseOptions> configureOptions) where TContext : DatabaseContext
    {
        var options = new DatabaseOptions();
        configureOptions.Invoke(options);
        BaseAddUnitOfWork<TContext>(services, options);
    }

    /// <summary>
    /// Registers the UnitOfWork pattern and DbContext in the dependency injection container with options configuration that relies on a service provider.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the UnitOfWork and DbContext will be added.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="IDatabaseOptions"/> with access to the <see cref="IServiceProvider"/>.</param>
    public static void AddUnitOfWork<TContext>(this IServiceCollection services,
        Action<IServiceProvider, IDatabaseOptions> configureOptions) where TContext : DatabaseContext
    {
        var serviceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(services);
        var options = new DatabaseOptions();
        configureOptions.Invoke(serviceProvider, options);
        BaseAddUnitOfWork<TContext>(services, options);
    }

    /// <summary>
    /// Registers the UnitOfWork pattern and DbContext with the specified lifetime and options.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the UnitOfWork and DbContext will be added.</param>
    /// <param name="options">The <see cref="IDatabaseOptions"/> that define the configuration and lifetime of the services.</param>
    private static void BaseAddUnitOfWork<TContext>(this IServiceCollection services, IDatabaseOptions options)
        where TContext : DatabaseContext
    {
        var time = options.ServiceLifetime;
        services.AddDbContext<DatabaseContext, TContext>(builder => { builder.UseDatabaseOptions(options); }, time);
        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), time));
    }
}