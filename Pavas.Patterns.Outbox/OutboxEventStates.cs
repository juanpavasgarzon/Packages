namespace Pavas.Patterns.Outbox;

public static class OutboxEventStates
{
    public const string Pending = "Pending";
    public const string Published = "Published";
    public const string Fail = "Fail";
}