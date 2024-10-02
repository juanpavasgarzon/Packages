# Minimal API Request Handler Pattern

This repository provides a pattern for registering and mapping request handlers in an ASP.NET Core Minimal API. The
pattern allows for modular organization of endpoints, where each request handler implements the `IRequestHandler`
interface and defines its own routes. This also supports global endpoint configuration such as adding filters.

## Features

- **Modular Endpoint Configuration**: Define your API routes in separate classes that implement `IRequestHandler`.
- **Automatic Registration**: Automatically register all implementations of `IRequestHandler` in the dependency
  injection container.
- **Global Configuration Support**: Apply global options (like filters) to all mapped endpoints.

## Getting Started

### Define a Request Handler

Create a class that implements `IRequestHandler` to define your API endpoints. Each handler is responsible for
configuring its own routes.

```csharp
using Microsoft.AspNetCore.Routing;
using Pavas.Patterns.MinimalApi.Contracts;

public class MyRequestHandler : IRequestHandler
{
    public RouteHandlerBuilder Configure(IEndpointRouteBuilder endpoints)
    {
        // Define an HTTP GET endpoint at "/myendpoint"
        return endpoints.MapGet("/myendpoint", () => "Hello from MyRequestHandler!");
    }
}
```

### Register And Map Request Handlers

In the `Configure` or `Program.cs` file, map all the registered request handlers to the ASP.NET Core routing system.

```csharp
using Pavas.Patterns.MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Automatically register all IRequestHandler implementations
builder.Services.AddRequestHandlers();

var app = builder.Build();

// Map all request handlers without global options
app.MapRequestHandlers();

app.Run();
```

### Applying Global Options (Optional)

You can also apply global options to all mapped endpoints, such as adding filters or additional configuration.

```csharp
app.MapRequestHandlers(builder =>
{
    // Apply a global filter to all routes
    builder.AddEndpointFilter(async (context, next) =>
    {
      Console.WriteLine("Global filter applied");
      return await next(context);
    });
});
```

## Benefits

- **Decoupled Routes**: Each request handler is independent, allowing for cleaner code organization.
- **Reusable Global Logic**: Easily apply cross-cutting concerns (like logging or validation) to all routes.

## License

This project is licensed under the MIT License.
