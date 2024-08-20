namespace Pavas.Patterns.Cqrs.Contracts;

public interface IQueryDispatcher
{
    public Task<TValue> DispatchAsync<TQuery, TValue>(TQuery query, CancellationToken cancellationToken = new());
    public Task<TValue> DispatchAsync<TValue>(CancellationToken cancellationToken = new());
    public TValue Dispatch<TQuery, TValue>(TQuery query);
    public TValue Dispatch<TValue>();
}