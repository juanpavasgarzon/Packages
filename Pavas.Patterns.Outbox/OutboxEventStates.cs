namespace Pavas.Patterns.Outbox;

/// <summary>
/// Defines the possible states for events in the outbox pattern.
/// </summary>
public static class OutboxEventStates
{
    /// <summary>
    /// Indicates that the event is pending and has not yet been published.
    /// </summary>
    public const string Pending = "Pending";

    /// <summary>
    /// Indicates that the event has been successfully published.
    /// </summary>
    public const string Published = "Published";

    /// <summary>
    /// Indicates that the event publishing has failed.
    /// </summary>
    public const string Fail = "Fail";
}