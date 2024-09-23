namespace Pavas.Patterns.Cqrs.Contracts;

/// <summary>
/// Defines an asynchronous handler for processing a command without returning a result.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
public interface ICommandHandlerAsync<in TCommand>
{
    /// <summary>
    /// Asynchronously handles the given command.
    /// </summary>
    /// <param name="command">The command to process.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = new());
}

/// <summary>
/// Defines an asynchronous handler for processing a command and returning a result.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
/// <typeparam name="TResult">The type of the result returned after processing the command.</typeparam>
public interface ICommandHandlerAsync<in TCommand, TResult>
{
    /// <summary>
    /// Asynchronously handles the given command and returns a result.
    /// </summary>
    /// <param name="command">The command to process.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the command.</returns>
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = new());
}