using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context;

/// <summary>
/// Provides an asynchronous local storage for a context of type <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TContext">The type of the context to store.</typeparam>
internal sealed class AsyncLocalContextProvider<TContext> : IContextProvider<TContext> where TContext : class
{
    /// <summary>
    /// Holds the context in asynchronous local storage, ensuring that the context is specific to each asynchronous flow.
    /// </summary>
    private static readonly AsyncLocal<TContext?> AsyncLocalContext = new();

    /// <summary>
    /// Gets or sets the current context from asynchronous local storage.
    /// </summary>
    /// <value>The current context of type <typeparamref name="TContext"/> or null if not set.</value>
    public TContext? Context
    {
        get => AsyncLocalContext.Value;
        set => AsyncLocalContext.Value = value;
    }
}