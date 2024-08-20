using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context;

public sealed class ContextProvider<TContext> : IContextProvider<TContext> where TContext : class
{
    private static readonly AsyncLocal<TContext?> AsyncLocalContext = new();

    public TContext? Context
    {
        get => AsyncLocalContext.Value;
        set => AsyncLocalContext.Value = value;
    }
}