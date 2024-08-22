using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context;

internal sealed class GlobalContextProvider<TContext> : IContextProvider<TContext> where TContext : class
{
    public TContext? Context { get; set; }

    public static GlobalContextProvider<TContext> GetInstance() => new();
}