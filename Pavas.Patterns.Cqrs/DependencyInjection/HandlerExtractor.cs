using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Cqrs.Contracts;

namespace Pavas.Patterns.Cqrs.DependencyInjection;

internal static class HandlerExtractor
{
    private static Type[] HandlerInterfaces =>
    [
        typeof(ICommandHandler<>),
        typeof(ICommandHandlerAsync<>),
        typeof(ICommandHandler<,>),
        typeof(ICommandHandlerAsync<,>),
        typeof(IQueryHandler<>),
        typeof(IQueryHandlerAsync<>),
        typeof(IQueryHandler<,>),
        typeof(IQueryHandlerAsync<,>)
    ];

    private static bool FilterInterfaces(Type serviceType)
    {
        return serviceType.IsGenericType && HandlerInterfaces.Contains(serviceType.GetGenericTypeDefinition());
    }

    private static ServiceDescriptor ServiceDescriptor(Type serviceType, Type implementationType)
    {
        return new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped);
    }

    internal static Type[] GetTypesFromAssembly(Assembly assembly)
    {
        return assembly!.GetTypes();
    }

    internal static bool FilterImplementations(Type type)
    {
        return type is { IsAbstract: false, IsInterface: false };
    }

    internal static IEnumerable<ServiceDescriptor> Descriptor(Type implementationType)
    {
        return implementationType.GetInterfaces()
            .Where(FilterInterfaces)
            .Select(serviceType => ServiceDescriptor(serviceType, implementationType));
    }
}