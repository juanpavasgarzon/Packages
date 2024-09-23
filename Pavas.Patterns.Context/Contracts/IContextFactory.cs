namespace Pavas.Patterns.Context.Contracts;

/// <summary>
/// Defines a factory interface for constructing and destructing a context of type <typeparamref name="TContext"/>.
/// </summary>
/// <typeparam name="TContext">The type of the context to construct and destruct.</typeparam>
public interface IContextFactory<in TContext> where TContext : class
{
    /// <summary>
    /// Constructs the given context.
    /// </summary>
    /// <param name="context">The context to be constructed.</param>
    public void Construct(TContext context);

    /// <summary>
    /// Destructs the current context, cleaning up any resources.
    /// </summary>
    public void Destruct();
}