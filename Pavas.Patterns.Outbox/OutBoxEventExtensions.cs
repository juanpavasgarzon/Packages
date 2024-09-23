using System.Text.Json;
using Pavas.Patterns.Outbox.Contracts;

namespace Pavas.Patterns.Outbox;

/// <summary>
/// Provides extension methods for working with outbox events, including setting event type, serializing payloads, and updating event status.
/// </summary>
public static class OutBoxEventExtensions
{
    /// <summary>
    /// Sets the event type for the given outbox event.
    /// </summary>
    /// <param name="outboxEvent">The outbox event on which to set the event type.</param>
    /// <param name="eventType">The event type to assign to the outbox event.</param>
    public static void EventType(this IOutboxEvent outboxEvent, string eventType)
    {
        outboxEvent.EventType = eventType;
    }

    /// <summary>
    /// Serializes the given payload and sets it as the payload of the outbox event.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload to serialize.</typeparam>
    /// <param name="outboxEvent">The outbox event on which to set the serialized payload.</param>
    /// <param name="payload">The payload to serialize and set.</param>
    public static void SerializePayload<TPayload>(this IOutboxEvent outboxEvent, TPayload payload)
    {
        outboxEvent.Payload = JsonSerializer.Serialize(payload);
    }

    /// <summary>
    /// Marks the outbox event as pending and sets the creation timestamp to the current UTC time.
    /// </summary>
    /// <param name="outboxEvent">The outbox event to update.</param>
    public static void MarkAsPending(this IOutboxEvent outboxEvent)
    {
        outboxEvent.Status = OutboxEventStates.Pending;
        outboxEvent.CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the outbox event as published and sets the published timestamp to the current UTC time.
    /// </summary>
    /// <param name="outboxEvent">The outbox event to update.</param>
    public static void MarkAsPublished(this IOutboxEvent outboxEvent)
    {
        outboxEvent.Status = OutboxEventStates.Published;
        outboxEvent.PublishedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the outbox event as failed and sets the failure timestamp to the current UTC time.
    /// </summary>
    /// <param name="outboxEvent">The outbox event to update.</param>
    public static void MarkAsFail(this IOutboxEvent outboxEvent)
    {
        outboxEvent.Status = OutboxEventStates.Fail;
        outboxEvent.FailedAt = DateTime.UtcNow;
    }
}