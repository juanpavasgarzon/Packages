namespace Pavas.Patterns.Cqrs.DependencyInjection;

internal static class HandlerExtractor
{
    private static bool MatchInterface(Type type, Type helper)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == helper;
    }

    private static Assignable AssemblyHandlerSelector(Type type, Type helper)
    {
        return new Assignable
        {
            Type = type,
            Interface = Array.Find(type.GetInterfaces(), match => MatchInterface(match, helper))
        };
    }

    internal static List<Assignable> Get(Type handler)
    {
        return handler.Assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Select(type => AssemblyHandlerSelector(type, handler))
            .Where(x => x.Interface != null)
            .ToList();
    }
}