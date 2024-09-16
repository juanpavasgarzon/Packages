namespace Pavas.Patterns.Outbox.Contracts;

public interface IOutboxRepository
{
    public Task AddAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    public Task<List<TEntity>> GetPendingEventsAsync<TEntity>(CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    public Task MarkEventAsPendingAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    public Task MarkEventAsPublishedAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    public Task MarkEventAsFailedAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;
}