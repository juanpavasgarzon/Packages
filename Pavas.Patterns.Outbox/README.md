
# Pavas.Patterns.Outbox

The `Pavas.Patterns.Outbox` NuGet package provides an interface and utility extensions for handling outbox pattern events. This package simplifies the process of handling event-driven systems by managing the state of events and their payloads through extensions, including marking events as pending, sent, or failed.

## Features

- Define an outbox event using the `IOutboxEvent` interface.
- Serialize and handle event payloads via extension methods.
- Track event states (`Pending`, `Published`, `Fail`) using a simple, intuitive API.
- Utilities to mark events as `Pending`, `Sent`, or `Fail`.
- Custom repository interface for managing event state in a database.

## Installation

To install the NuGet package, run the following command in the .NET CLI:

```bash
dotnet add package Pavas.Patterns.Outbox
```

Or through the NuGet Package Manager:

```bash
Install-Package Pavas.Patterns.Outbox
```

## Usage

### Define Outbox Events

Implement the `IOutboxEvent` interface in your event classes:

```csharp
namespace MyApp.Events
{
    public class MyOutboxEvent : IOutboxEvent
    {
        public int Id { get; init; }
        public string EventType { get; set; }
        public string Payload { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? FailedAt { get; set; }
    }
}
```

### Serialize Payloads

Use the extension method `SerializePayload` to serialize your event payload:

```csharp
var myEvent = new MyOutboxEvent();
myEvent.SerializePayload(new { Message = "Hello, World!" });
```

### Mark Event Status

Update the event's status using the provided extension methods:

- Mark as **Pending**:

```csharp
myEvent.MarkAsPending();
```

- Mark as **Sent**:

```csharp
myEvent.MarkAsSent();
```

- Mark as **Failed**:

```csharp
myEvent.MarkAsFail();
```

### Outbox Repository Interface

Use the `IOutboxRepository` interface to handle events in the outbox pattern:

```csharp
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
```

## Event States

The outbox event can have the following states:

- **Pending**: The event is ready to be processed.
- **Published**: The event has been successfully processed and published.
- **Fail**: The event processing has failed.

## Contributing

Feel free to open issues or pull requests if you encounter bugs or have suggestions for improvements.

## License

This project is licensed under the MIT License.
