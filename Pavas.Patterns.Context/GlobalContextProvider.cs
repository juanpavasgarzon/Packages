using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context;

/// <summary>
/// Provides a global, non-thread-specific storage for a context of type <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TContext">The type of the context to store.</typeparam>
internal sealed class GlobalContextProvider<TContext> : IContextProvider<TContext> where TContext : class
{
    /// <summary>
    /// Gets or sets the globally shared context.
    /// </summary>
    /// <value>The current context of type <typeparamref name="TContext"/> or null if not set.</value>
    public TContext? Context { get; set; }

    /// <summary>
    /// Creates and returns a new instance of the <see cref="GlobalContextProvider{TContext}"/> class.
    /// </summary>
    /// <returns>A new instance of <see cref="GlobalContextProvider{TContext}"/>.</returns>
    public static GlobalContextProvider<TContext> GetInstance() => new();
}