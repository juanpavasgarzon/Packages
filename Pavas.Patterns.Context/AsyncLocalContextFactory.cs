using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context;

/// <summary>
/// Provides a factory for constructing and destructing a context in an asynchronous local storage using <see cref="IContextProvider{TContext}"/>.
/// </summary>
/// <typeparam name="TContext">The type of the context to manage.</typeparam>
internal sealed class AsyncLocalContextFactory<TContext> : IContextFactory<TContext> where TContext : class
{
    private readonly IContextProvider<TContext> _contextProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLocalContextFactory{TContext}"/> class with a context provider.
    /// </summary>
    /// <param name="contextProvider">The provider that stores the context asynchronously.</param>
    public AsyncLocalContextFactory(IContextProvider<TContext> contextProvider)
    {
        _contextProvider = contextProvider;
    }

    /// <summary>
    /// Constructs the given context by setting it in the context provider.
    /// </summary>
    /// <param name="context">The context to set.</param>
    public void Construct(TContext context)
    {
        _contextProvider.Context = context;
    }

    /// <summary>
    /// Destructs the current context by clearing it in the context provider.
    /// </summary>
    public void Destruct()
    {
        _contextProvider.Context = null;
    }
}