namespace Pavas.Patterns.Outbox;

public interface IOutboxEvent
{
    public int Id { get; init; }
    public string EventType { get; set; }
    public string Payload { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? FailedAt { get; set; }
}