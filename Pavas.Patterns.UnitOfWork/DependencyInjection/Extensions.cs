using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.UnitOfWork.Abstracts;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.Contracts.Options;
using Pavas.Patterns.UnitOfWork.Exceptions;
using Pavas.Patterns.UnitOfWork.Extensions;
using Pavas.Patterns.UnitOfWork.Options;
using Pavas.Patterns.UnitOfWork.Options.Extensions;

namespace Pavas.Patterns.UnitOfWork.DependencyInjection;

/// <summary>
/// Provides extension methods to register UnitOfWork and DbContext services in the dependency injection container.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Registers the UnitOfWork pattern and DbContext in the dependency injection container using a specified configurator and service lifetime.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context to register.</typeparam>
    /// <typeparam name="TConfigurator">
    /// The type of the configurator that implements <see cref="IUnitOfWorkConfigurator"/> to provide the configuration logic.
    /// </typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the UnitOfWork and DbContext will be added.</param>
    /// <param name="serviceLifetime">The lifetime (Scoped, Singleton, Transient) for the UnitOfWork and DbContext services.</param>
    public static void AddUnitOfWork<TContext, TConfigurator>(this IServiceCollection services,
        ServiceLifetime serviceLifetime) where TContext : DatabaseContext
        where TConfigurator : class, IUnitOfWorkConfigurator, new()
    {
        services.AddDbContext<DatabaseContext, TContext>((provider, builder) =>
        {
            var configurator = new TConfigurator();
            var options = configurator.Configure(provider, new DatabaseOptions());

            if (options.ConnectionString.IsNullOrEmptyOrWhiteSpace())
                throw new RequireMemberException("ConnectionString is required in implementation");

            builder.UseDatabaseOptions(options);
        }, serviceLifetime, serviceLifetime);

        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), serviceLifetime));
    }

    /// <summary>
    /// Registers the UnitOfWork pattern and DbContext with a specified lifetime and configuration options.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the UnitOfWork and DbContext will be added.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="IDatabaseOptions"/> with access to the <see cref="IServiceProvider"/>.</param>
    /// <param name="serviceLifetime">The lifetime (singleton, scoped, transient) for the UnitOfWork and DbContext services.</param>
    public static void AddUnitOfWork<TContext>(this IServiceCollection services,
        Action<IServiceProvider, IDatabaseOptions> configureOptions,
        ServiceLifetime serviceLifetime) where TContext : DatabaseContext
    {
        services.AddDbContext<DatabaseContext, TContext>((provider, builder) =>
        {
            var options = new DatabaseOptions();
            configureOptions.Invoke(provider, options);

            if (options.ConnectionString.IsNullOrEmptyOrWhiteSpace())
                throw new RequireMemberException("ConnectionString is required in implementation");

            builder.UseDatabaseOptions(options);
        }, serviceLifetime, serviceLifetime);

        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), serviceLifetime));
    }

    /// <summary>
    /// Registers the UnitOfWork pattern and DbContext with a specified lifetime and configuration options.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the UnitOfWork and DbContext will be added.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="IDatabaseOptions"/>.</param>
    /// <param name="serviceLifetime">The lifetime (singleton, scoped, transient) for the UnitOfWork and DbContext services.</param>
    public static void AddUnitOfWork<TContext>(this IServiceCollection services,
        Action<IDatabaseOptions> configureOptions,
        ServiceLifetime serviceLifetime) where TContext : DatabaseContext
    {
        services.AddDbContext<DatabaseContext, TContext>(builder =>
        {
            var options = new DatabaseOptions();
            configureOptions.Invoke(options);

            if (options.ConnectionString.IsNullOrEmptyOrWhiteSpace())
                throw new RequireMemberException("ConnectionString is required in implementation");

            builder.UseDatabaseOptions(options);
        }, serviceLifetime, serviceLifetime);

        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), serviceLifetime));
    }
}