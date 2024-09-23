namespace Pavas.Patterns.Cqrs.Contracts;

/// <summary>
/// Defines a dispatcher interface for executing commands in the CQRS pattern.
/// </summary>
public interface ICommandDispatcher
{
    /// <summary>
    /// Asynchronously dispatches a command and returns a result of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to dispatch.</typeparam>
    /// <typeparam name="TValue">The return type of the result produced by the command.</typeparam>
    /// <param name="command">The command to dispatch.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the command.</returns>
    public Task<TValue> DispatchAsync<TCommand, TValue>(TCommand command, CancellationToken cancellationToken = new());

    /// <summary>
    /// Asynchronously dispatches a command without returning a result.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to dispatch.</typeparam>
    /// <param name="command">The command to dispatch.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = new());

    /// <summary>
    /// Dispatches a command synchronously and returns a result of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to dispatch.</typeparam>
    /// <typeparam name="TValue">The return type of the result produced by the command.</typeparam>
    /// <param name="command">The command to dispatch.</param>
    /// <returns>The result of type <typeparamref name="TValue"/> produced by the command.</returns>
    public TValue Dispatch<TCommand, TValue>(TCommand command);

    /// <summary>
    /// Dispatches a command synchronously without returning a result.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to dispatch.</typeparam>
    /// <param name="command">The command to dispatch.</param>
    public void Dispatch<TCommand>(TCommand command);
}