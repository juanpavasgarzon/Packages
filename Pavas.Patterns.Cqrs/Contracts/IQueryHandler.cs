namespace Pavas.Patterns.Cqrs.Contracts;

/// <summary>
/// Defines a handler for processing a query and returning a result.
/// </summary>
/// <typeparam name="TQuery">The type of the query to handle.</typeparam>
/// <typeparam name="TResult">The type of the result returned after processing the query.</typeparam>
public interface IQueryHandler<in TQuery, out TResult>
{
    /// <summary>
    /// Handles the given query and returns a result.
    /// </summary>
    /// <param name="query">The query to process.</param>
    /// <returns>The result produced after handling the query.</returns>
    TResult Handle(TQuery query);
}

/// <summary>
/// Defines a handler for processing a query without parameters and returning a result.
/// </summary>
/// <typeparam name="TResult">The type of the result returned after processing the query.</typeparam>
public interface IQueryHandler<out TResult>
{
    /// <summary>
    /// Handles the query and returns a result.
    /// </summary>
    /// <returns>The result produced after handling the query.</returns>
    TResult Handle();
}