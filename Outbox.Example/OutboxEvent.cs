using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pavas.Patterns.Outbox.Contracts;

namespace Outbox.Example;

public class OutboxEvent : IOutboxEvent
{
    [Key] public int Id { get; set; }
    [MaxLength(255)] public string EventType { get; set; } = string.Empty;
    [MaxLength(1000000)] public string Payload { get; set; } = string.Empty;
    [MaxLength(255)] public string Status { get; set; } = string.Empty;
    [MaxLength(255)] public DateTime CreatedAt { get; set; }
    [MaxLength(255)] public DateTime? PublishedAt { get; set; }
    [MaxLength(255)] public DateTime? FailedAt { get; set; }
}