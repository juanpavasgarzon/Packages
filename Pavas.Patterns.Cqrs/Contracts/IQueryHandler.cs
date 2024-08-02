namespace Pavas.Patterns.Cqrs.Contracts;

public interface IQueryHandler<in TQuery, out TResult>
{
    TResult Handle(TQuery query);
}

public interface IQueryHandler<out TResult>
{
    TResult Handle();
}