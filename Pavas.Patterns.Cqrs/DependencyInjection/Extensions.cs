using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Cqrs.Contracts;

namespace Pavas.Patterns.Cqrs.DependencyInjection;

public static class Extensions
{
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