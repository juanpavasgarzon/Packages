using System.Text.Json;
using Pavas.Patterns.Outbox.Contracts;

namespace Pavas.Patterns.Outbox;

public static class OutBoxEventExtensions
{
    public static void EventType(this IOutboxEvent outboxEvent, string eventType)
    {
        outboxEvent.EventType = eventType;
    }

    public static void SerializePayload<TPayload>(this IOutboxEvent outboxEvent, TPayload payload)
    {
        outboxEvent.Payload = JsonSerializer.Serialize(payload);
    }

    public static void MarkAsPending(this IOutboxEvent outboxEvent)
    {
        outboxEvent.Status = OutboxEventStates.Pending;
        outboxEvent.CreatedAt = DateTime.UtcNow;
    }

    public static void MarkAsSent(this IOutboxEvent outboxEvent)
    {
        outboxEvent.Status = OutboxEventStates.Published;
        outboxEvent.PublishedAt = DateTime.UtcNow;
    }

    public static void MarkAsFail(this IOutboxEvent outboxEvent)
    {
        outboxEvent.Status = OutboxEventStates.Fail;
        outboxEvent.FailedAt = DateTime.UtcNow;
    }
}