namespace Pavas.Patterns.Cqrs.Contracts;

/// <summary>
/// Defines a dispatcher interface for executing queries in the CQRS pattern.
/// </summary>
public interface IQueryDispatcher
{
    /// <summary>
    /// Asynchronously dispatches a query and returns a result of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to dispatch.</typeparam>
    /// <typeparam name="TValue">The return type of the result produced by the query.</typeparam>
    /// <param name="query">The query to dispatch.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the query.</returns>
    public Task<TValue> DispatchAsync<TQuery, TValue>(TQuery query, CancellationToken cancellationToken = new());

    /// <summary>
    /// Asynchronously dispatches a query without parameters and returns a result of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The return type of the result produced by the query.</typeparam>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the query.</returns>
    public Task<TValue> DispatchAsync<TValue>(CancellationToken cancellationToken = new());

    /// <summary>
    /// Dispatches a query synchronously and returns a result of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to dispatch.</typeparam>
    /// <typeparam name="TValue">The return type of the result produced by the query.</typeparam>
    /// <param name="query">The query to dispatch.</param>
    /// <returns>The result of type <typeparamref name="TValue"/> produced by the query.</returns>
    public TValue Dispatch<TQuery, TValue>(TQuery query);

    /// <summary>
    /// Dispatches a query synchronously without parameters and returns a result of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The return type of the result produced by the query.</typeparam>
    /// <returns>The result of type <typeparamref name="TValue"/> produced by the query.</returns>
    public TValue Dispatch<TValue>();
}