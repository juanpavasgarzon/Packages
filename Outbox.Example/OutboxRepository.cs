using Microsoft.EntityFrameworkCore;
using Pavas.Patterns.Outbox;
using Pavas.Patterns.Outbox.Contracts;

namespace Outbox.Example;

public class OutboxRepository : IOutboxRepository
{
    private readonly DatabaseContext _databaseContext = new("Outbox");

    public async Task AddAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent
    {
        @event.MarkAsPending();
        await _databaseContext.Set<TEntity>().AddAsync(@event, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetPendingEventsAsync<TEntity>(CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent
    {
        return await _databaseContext.Set<TEntity>()
            .Where(@event => @event.Status == OutboxEventStates.Pending)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkEventAsPendingAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent
    {
        @event.MarkAsPending();
        _databaseContext.Set<TEntity>().Update(@event);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task MarkEventAsPublishedAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent
    {
        @event.MarkAsPublished();
        _databaseContext.Set<TEntity>().Update(@event);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task MarkEventAsFailedAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent
    {
        @event.MarkAsFail();
        _databaseContext.Set<TEntity>().Update(@event);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}