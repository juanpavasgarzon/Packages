using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Cqrs.Contracts;

namespace Pavas.Patterns.Cqrs.DependencyInjection;

/// <summary>
/// Provides extension methods for adding CQRS-related services to the dependency injection container.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Adds the CQRS infrastructure to the service collection, including command and query dispatchers,
    /// and automatically registers all command and query handlers found in referenced assemblies.
    /// </summary>
    /// <param name="serviceCollection">The service collection to which the CQRS services will be added.</param>
    public static void AddCqrs(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICommandDispatcher, CommandDispatcher>();
        serviceCollection.AddScoped<IQueryDispatcher, QueryDispatcher>();

        var services = Assembly.GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Select(Assembly.Load)
            .Append(Assembly.GetEntryAssembly()!)
            .SelectMany(HandlerExtractor.GetTypesFromAssembly)
            .Where(HandlerExtractor.FilterImplementations)
            .SelectMany(HandlerExtractor.Descriptor);

        foreach (var service in services)
        {
            serviceCollection.Add(service);
        }
    }
}