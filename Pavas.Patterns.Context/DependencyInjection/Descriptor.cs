using Microsoft.Extensions.DependencyInjection;

namespace Pavas.Patterns.Context.DependencyInjection;

internal sealed class Descriptor<TInterface, TImplementation>(
    ServiceLifetime serviceLifetime
) : ServiceDescriptor(typeof(TInterface), typeof(TImplementation), serviceLifetime);