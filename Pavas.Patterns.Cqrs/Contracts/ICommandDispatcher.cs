namespace Pavas.Patterns.Cqrs.Contracts;

public interface ICommandDispatcher
{
    public Task<TValue> DispatchAsync<TCommand, TValue>(TCommand command);
    public Task DispatchAsync<TCommand>(TCommand command);
    public TValue Dispatch<TCommand, TValue>(TCommand command);
    public void Dispatch<TCommand>(TCommand command);
}