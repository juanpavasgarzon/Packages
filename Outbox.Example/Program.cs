using System.Text.Json;
using Outbox.Example;
using Pavas.Patterns.Outbox;
using Pavas.Patterns.Outbox.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOutboxRepository, OutboxRepository>();

var app = builder.Build();
var repository = app.Services.GetRequiredService<IOutboxRepository>();

var @event = new OutboxEvent
{
    EventType = "my-event",
    Payload = JsonSerializer.Serialize(new { Message = "Hello World" }),
    Status = OutboxEventStates.Pending,
    CreatedAt = DateTime.UtcNow,
};

await repository.AddAsync(@event);

var events = await repository.GetPendingEventsAsync<OutboxEvent>();

Console.WriteLine(JsonSerializer.Serialize(events));

foreach (var exampleEvent in events)
{
    await repository.MarkEventAsPublishedAsync(exampleEvent);
}

Console.WriteLine(JsonSerializer.Serialize(events));

events = await repository.GetPendingEventsAsync<OutboxEvent>();

Console.WriteLine(JsonSerializer.Serialize(events));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();