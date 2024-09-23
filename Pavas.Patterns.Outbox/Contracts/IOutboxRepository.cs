namespace Pavas.Patterns.Outbox.Contracts;

/// <summary>
/// Defines a repository interface for handling outbox events, providing methods to add, retrieve, and update events based on their state.
/// </summary>
public interface IOutboxRepository
{
    /// <summary>
    /// Adds a new outbox event asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the outbox event entity.</typeparam>
    /// <param name="event">The event to add to the repository.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task AddAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    /// <summary>
    /// Retrieves all pending outbox events asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the outbox event entity.</typeparam>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing a list of pending events.</returns>
    public Task<List<TEntity>> GetPendingEventsAsync<TEntity>(CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    /// <summary>
    /// Marks an outbox event as pending asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the outbox event entity.</typeparam>
    /// <param name="event">The event to mark as pending.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task MarkEventAsPendingAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    /// <summary>
    /// Marks an outbox event as published asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the outbox event entity.</typeparam>
    /// <param name="event">The event to mark as published.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task MarkEventAsPublishedAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;

    /// <summary>
    /// Marks an outbox event as failed asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the outbox event entity.</typeparam>
    /// <param name="event">The event to mark as failed.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task MarkEventAsFailedAsync<TEntity>(TEntity @event, CancellationToken cancellationToken = new())
        where TEntity : class, IOutboxEvent;
}