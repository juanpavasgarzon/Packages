# Pavas.Patterns.Context

Pavas.Patterns.Context is a .NET library for managing context constructors and accessors (providers). It provides a
flexible and extensible way to create, manage, and access contexts in your applications. This library is designed with
dependency injection in mind, making it easy to integrate into existing projects.

## Installation

To install this library, add the following NuGet package to your project:

```bash
dotnet add package Pavas.Patterns.Context
```

## Usage

### 1. Define Context Class

Create a context class that will be managed by the context provider and factory. This class should contain any
information relevant to the context you want to manage.

```csharp
public class MyContext
{
    public string UserId { get; set; }
    public string TenantId { get; set; }
    public string CorrelationId { get; set; }

    public MyContext(string userId, string tenantId, string correlationId)
    {
        UserId = userId;
        TenantId = tenantId;
        CorrelationId = correlationId;
    }
}
```

### 2. Register the Context in Dependency Injection

In your `Startup.cs` or wherever you configure your services, register the context using the provided extension method.

Use the `ServiceLifetime` enum to assign the injection type.

#### For Scoped Context:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddScopedContext<MyContext>();
}
```

#### For Transient Context:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransientContext<MyContext>();
}
```

#### For Singleton Context:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var myContext = new MyContext("user-123", "tenant-456", "correlation-789");
    services.AddSingletonContext(myContext);
}
```

Or with an initializer:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingletonContext(provider =>
    {
        return new MyContext("user-123", "tenant-456", "correlation-789");
    });
}
```

### 3. Create and Use Contexts

Use the `IContextFactory<TContext>` to create (construct) and destroy (destruct) contexts within your application.
The `IContextProvider<TContext>` will allow you to access the current context.

#### Constructing a Context

```csharp
public class SomeService
{
    private readonly IContextFactory<MyContext> _contextFactory;

    public SomeService(IContextFactory<MyContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public void CreateContext()
    {
        var context = new MyContext("user-123", "tenant-456", "correlation-456");
        _contextFactory.Construct(context);
    }
}
```

#### Accessing the Context

```csharp
public class AnotherService
{
    private readonly IContextProvider<MyContext> _contextProvider;

    public AnotherService(IContextProvider<MyContext> contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public void DoSomethingWithContext()
    {
        var context = _contextProvider.Context;
        Console.WriteLine($"UserId: {context.UserId}, TenantId: {context.TenantId}, CorrelationId: {context.CorrelationId}");
    }
}
```

#### Destroying a Context

```csharp
public class CleanupService
{
    private readonly IContextFactory<MyContext> _contextFactory;

    public CleanupService(IContextFactory<MyContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public void Cleanup()
    {
        _contextFactory.Destruct();
    }
}
```

## Project Structure

The library is organized into several key components:

- `AsyncLocalContextProvider<TContext>`: Manages the current instance of the context for the current async execution
  context.
- `GlobalContextProvider<TContext>`: Manages a global or singleton instance of the context.
- `ContextFactory<TContext>`: Provides methods to create (construct) and destroy (destruct) contexts.
- `Extensions`: Contains methods for registering the context provider and factory with the dependency injection
  container.

## Contributing

Contributions to this library are welcome. Feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License.
