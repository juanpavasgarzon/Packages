namespace Pavas.Patterns.Cqrs.Contracts;

public interface IQueryHandlerAsync<TResult>
{
    Task<TResult> HandleAsync(CancellationToken cancellationToken = new());
}

public interface IQueryHandlerAsync<in TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = new());
}