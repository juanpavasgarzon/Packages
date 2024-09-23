namespace Pavas.Patterns.Cqrs.Contracts;

/// <summary>
/// Defines an asynchronous handler for processing a query without parameters and returning a result.
/// </summary>
/// <typeparam name="TResult">The type of the result returned after processing the query.</typeparam>
public interface IQueryHandlerAsync<TResult>
{
    /// <summary>
    /// Asynchronously handles the query and returns a result.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the query.</returns>
    Task<TResult> HandleAsync(CancellationToken cancellationToken = new());
}

/// <summary>
/// Defines an asynchronous handler for processing a query and returning a result.
/// </summary>
/// <typeparam name="TQuery">The type of the query to handle.</typeparam>
/// <typeparam name="TResult">The type of the result returned after processing the query.</typeparam>
public interface IQueryHandlerAsync<in TQuery, TResult>
{
    /// <summary>
    /// Asynchronously handles the given query and returns a result.
    /// </summary>
    /// <param name="query">The query to process.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the query.</returns>
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = new());
}