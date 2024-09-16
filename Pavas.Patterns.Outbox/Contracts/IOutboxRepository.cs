namespace Pavas.Patterns.Outbox.Contracts;

public interface IOutboxRepository
{
    Task<IEnumerable<TEvent>> GetPendingEventsAsync<TEvent>(
        CancellationToken cancellationToken = new()
    ) where TEvent : IOutboxEvent;

    Task SetEventAsProcessingAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = new()
    ) where TEvent : IOutboxEvent;

    Task SetEventAsFailedAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = new()
    ) where TEvent : IOutboxEvent;

    Task SetEventAsPublishedAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = new()
    ) where TEvent : IOutboxEvent;
}