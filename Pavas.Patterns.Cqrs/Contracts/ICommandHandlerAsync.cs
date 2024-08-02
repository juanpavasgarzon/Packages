namespace Pavas.Patterns.Cqrs.Contracts;

public interface ICommandHandlerAsync<in TCommand>
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = new());
}

public interface ICommandHandlerAsync<in TCommand, TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = new());
}