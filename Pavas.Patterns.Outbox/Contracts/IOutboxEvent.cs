namespace Pavas.Patterns.Outbox.Contracts;

/// <summary>
/// Defines the structure of an outbox event, including details such as event type, payload, status, and timestamps for tracking the event's state.
/// </summary>
public interface IOutboxEvent
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    /// <value>An integer representing the event's unique identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    /// <value>A string representing the type of event.</value>
    public string EventType { get; set; }

    /// <summary>
    /// Gets or sets the serialized payload of the event.
    /// </summary>
    /// <value>A string containing the serialized data of the event.</value>
    public string Payload { get; set; }

    /// <summary>
    /// Gets or sets the current status of the event (e.g., Pending, Published, Fail).
    /// </summary>
    /// <value>A string representing the current state of the event.</value>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the event was created.
    /// </summary>
    /// <value>A <see cref="DateTime"/> representing the creation time in UTC.</value>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the event was published.
    /// </summary>
    /// <value>A nullable <see cref="DateTime"/> representing the time of publication, or null if the event has not been published.</value>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the event failed to be published.
    /// </summary>
    /// <value>A nullable <see cref="DateTime"/> representing the time of failure, or null if the event has not failed.</value>
    public DateTime? FailedAt { get; set; }
}