namespace Pavas.Patterns.Context.Contracts;

/// <summary>
/// Defines an interface for providing access to a context of type <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TContext">The type of the context to provide.</typeparam>
public interface IContextProvider<TContext> where TContext : class
{
    /// <summary>
    /// Gets or sets the current context.
    /// </summary>
    /// <value>The current context of type <typeparamref name="TContext"/> or null if not set.</value>
    public TContext? Context { get; set; }
}