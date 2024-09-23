namespace Pavas.Patterns.Cqrs.Contracts;

/// <summary>
/// Defines a handler for processing a command without returning a result.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
public interface ICommandHandler<in TCommand>
{
    /// <summary>
    /// Handles the given command.
    /// </summary>
    /// <param name="command">The command to process.</param>
    void Handle(TCommand command);
}

/// <summary>
/// Defines a handler for processing a command and returning a result.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
/// <typeparam name="TResult">The type of the result returned after processing the command.</typeparam>
public interface ICommandHandler<in TCommand, out TResult>
{
    /// <summary>
    /// Handles the given command and returns a result.
    /// </summary>
    /// <param name="command">The command to process.</param>
    /// <returns>The result produced after handling the command.</returns>
    TResult Handle(TCommand command);
}