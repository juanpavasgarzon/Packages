using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.MinimalApi.Contracts;

namespace Pavas.Patterns.MinimalApi.Extensions;

/// <summary>
/// Provides extension methods for registering request handlers in the service collection.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Registers all implementations of IRequestHandler found in the application's assemblies as scoped services.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the request handler implementations will be added.</param>
    public static void AddRequestHandlers(this IServiceCollection services)
    {
        var handlerType = typeof(IRequestHandler);

        var implementations = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => handlerType.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false });

        foreach (var implementationType in implementations)
        {
            services.AddScoped(handlerType, implementationType);
        }
    }
}