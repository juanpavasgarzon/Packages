# CQRS Design Pattern in .NET

## Overview

The CQRS (Command Query Responsibility Segregation) pattern is a design pattern that separates the responsibilities of
commands and queries in a system. Commands are used to modify the state, while queries are used to retrieve data. This
separation leads to a more maintainable and scalable architecture.

## Interfaces

### Command Interfaces

- **ICommandDispatcher**: Dispatches commands to their respective handlers.
- **ICommandHandler**: Handles commands and optionally returns a result.
- **ICommandHandlerAsync**: An asynchronous version of `ICommandHandler`.

```csharp
public interface ICommandDispatcher
{
    Task<TValue> DispatchCommandAsync<TCommand, TValue>(TCommand command);
    Task DispatchCommandAsync<TCommand>(TCommand command);
    TValue DispatchCommand<TCommand, TValue>(TCommand command);
    void DispatchCommand<TCommand>(TCommand command);
}

public interface ICommandHandler<in TCommand>
{
    void Handle(TCommand command);
}

public interface ICommandHandler<in TCommand, out TResult>
{
    TResult Handle(TCommand command);
}

public interface ICommandHandlerAsync<in TCommand>
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = new());
}

public interface ICommandHandlerAsync<in TCommand, TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = new());
}
```

### Query Interfaces

- **IQueryDispatcher**: Dispatches queries to their respective handlers.
- **IQueryHandler**: Handles queries and returns a result.
- **IQueryHandlerAsync**: An asynchronous version of IQueryHandler.

```csharp
public interface IQueryDispatcher
{
    Task<TValue> DispatchQueryAsync<TQuery, TValue>(TQuery query);
    Task<TValue> DispatchQueryAsync<TValue>();
    TValue DispatchQuery<TQuery, TValue>(TQuery query);
    TValue DispatchQuery<TValue>();
}

public interface IQueryHandler<in TQuery, out TResult>
{
    TResult Handle(TQuery query);
}

public interface IQueryHandler<out TResult>
{
    TResult Handle();
}

public interface IQueryHandlerAsync<TResult>
{
    Task<TResult> HandleAsync(CancellationToken cancellationToken = new());
}

public interface IQueryHandlerAsync<in TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = new());
}
```

## Example Usage

### Dispatching Queries

```csharp
var queryDispatcher = serviceProvider.GetService<IQueryDispatcher>();

// Example async with query parameters 
var resultWithParams = await queryDispatcher.DispatchAsync<SomeQuery, SomeResult>(new SomeQuery());

// Example async without query parameters 
var resultWithoutParams = await queryDispatcher.DispatchAsync<SomeResult>()
    
// Example with query parameters
var resultWithParams = queryDispatcher.Dispatch<SomeQuery, SomeResult>(new SomeQuery());

// Example without query parameters
var resultWithoutParams = queryDispatcher.Dispatch<SomeResult>()
```

### Dispatching Queries

```csharp
var commandDispatcher = serviceProvider.GetService<ICommandDispatcher>();

// Example async with command response 
var resultWithParams = await commandDispatcher.DispatchAsync<SomeCommand, SomeResult>(new SomeCommand());

// Example async without command response 
await commandDispatcher.DispatchAsync<SomeCommand>(new SomeCommand())
    
// Example with command response
var resultWithParams = commandDispatcher.Dispatch<SomeCommand, SomeResult>(new SomeCommand());

// Example without command response
commandDispatcher.Dispatch<SomeCommand>(new SomeCommand())
```

### Implementing Query Handlers

```csharp
public class MyQueryHandler : IQueryHandlerAsync<SomeQuery, SomeResult>
{
    public async Task<SomeResult> HandleAsync(SomeQuery query, CancellationToken cancellationToken = new())
    {
        // Handle the query and return the result
    }
}

public class MyQueryHandler : IQueryHandler<SomeQuery, SomeResult>
{
    public SomeResult Handle(SomeQuery query)
    {
        // Handle the query and return the result
    }
}
```

### Implementing Command Handlers

```csharp
public class MyCommandHandler : ICommandHandlerAsync<SomeCommand, SomeResult>
{
    public async Task<SomeResult> HandleAsync(SomeCommand command, CancellationToken cancellationToken = new())
    {
        // Handle the command and return the result (if any)
    }
}

public class MyCommandHandler : ICommandHandler<SomeCommand, SomeResult>
{
    public SomeResult Handle(SomeCommand command)
    {
        // Handle the command and return the result (if any)
    }
}
```

## Dependency Injection

```csharp
// Register
serviceCollection.AddCqrs();
```

## Conclusion

The CQRS pattern provides a clear separation between commands and queries, making your application more maintainable and
scalable. By implementing the interfaces and extension methods described above, you can easily integrate CQRS into your
.NET applications.