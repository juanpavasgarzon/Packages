using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context;

internal sealed class AsyncLocalContextFactory<TContext>(
    IContextProvider<TContext> contextProvider
) : IContextFactory<TContext> where TContext : class
{
    public void Construct(TContext context)
    {
        contextProvider.Context = context;
    }

    public void Destruct()
    {
        contextProvider.Context = null;
    }
}