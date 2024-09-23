using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Cqrs.Contracts;

namespace Pavas.Patterns.Cqrs.DependencyInjection;

/// <summary>
/// Provides methods for extracting and filtering handler implementations for commands and queries from assemblies.
/// </summary>
internal static class HandlerExtractor
{
    /// <summary>
    /// Defines the interfaces for command and query handlers.
    /// </summary>
    private static Type[] HandlerInterfaces => new[]
    {
        typeof(ICommandHandler<>),
        typeof(ICommandHandlerAsync<>),
        typeof(ICommandHandler<,>),
        typeof(ICommandHandlerAsync<,>),
        typeof(IQueryHandler<>),
        typeof(IQueryHandlerAsync<>),
        typeof(IQueryHandler<,>),
        typeof(IQueryHandlerAsync<,>)
    };

    /// <summary>
    /// Filters the interfaces to check if they are command or query handler interfaces.
    /// </summary>
    /// <param name="serviceType">The interface type to filter.</param>
    /// <returns>True if the type is a valid handler interface; otherwise, false.</returns>
    private static bool FilterInterfaces(Type serviceType)
    {
        return serviceType.IsGenericType && HandlerInterfaces.Contains(serviceType.GetGenericTypeDefinition());
    }

    /// <summary>
    /// Creates a service descriptor for the given service and implementation types.
    /// </summary>
    /// <param name="serviceType">The service interface type.</param>
    /// <param name="implementationType">The implementation type that provides the service.</param>
    /// <returns>A <see cref="ServiceDescriptor"/> for registration in the service collection.</returns>
    private static ServiceDescriptor ServiceDescriptor(Type serviceType, Type implementationType)
    {
        return new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped);
    }

    /// <summary>
    /// Retrieves all types from a given assembly.
    /// </summary>
    /// <param name="assembly">The assembly from which to retrieve types.</param>
    /// <returns>An array of types from the specified assembly.</returns>
    internal static Type[] GetTypesFromAssembly(Assembly assembly)
    {
        return assembly.GetTypes();
    }

    /// <summary>
    /// Filters types to ensure they are concrete (non-abstract) and non-interface implementations.
    /// </summary>
    /// <param name="type">The type to filter.</param>
    /// <returns>True if the type is a concrete implementation; otherwise, false.</returns>
    internal static bool FilterImplementations(Type type)
    {
        return type is { IsAbstract: false, IsInterface: false };
    }

    /// <summary>
    /// Generates service descriptors for each interface implemented by the given type that matches the handler interfaces.
    /// </summary>
    /// <param name="implementationType">The implementation type to inspect for handler interfaces.</param>
    /// <returns>An IEnumerable of <see cref="ServiceDescriptor"/> for each valid handler interface implemented by the type.</returns>
    internal static IEnumerable<ServiceDescriptor> Descriptor(Type implementationType)
    {
        return implementationType.GetInterfaces()
            .Where(FilterInterfaces)
            .Select(serviceType => ServiceDescriptor(serviceType, implementationType));
    }
}