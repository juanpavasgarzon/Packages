namespace Pavas.Patterns.Cqrs.DependencyInjection;

internal sealed class Assignable
{
    internal required Type Type { get; init; }
    internal Type? Interface { get; init; }
}