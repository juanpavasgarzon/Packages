namespace Pavas.Patterns.Cqrs.Contracts;

public interface IQueryDispatcher
{
    public Task<TValue> DispatchAsync<TQuery, TValue>(TQuery query);
    public Task<TValue> DispatchAsync<TValue>();
    public TValue Dispatch<TQuery, TValue>(TQuery query);
    public TValue Dispatch<TValue>();
}