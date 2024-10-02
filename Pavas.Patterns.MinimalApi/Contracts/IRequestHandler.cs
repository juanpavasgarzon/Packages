using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Pavas.Patterns.MinimalApi.Contracts;

/// <summary>
/// Defines a contract for request handlers that can configure 
/// and map endpoints within an ASP.NET Core application.
/// Each implementing class will define its own endpoints and 
/// return a RouteHandlerBuilder for further configuration.
/// </summary>
public interface IRequestHandler
{
    /// <summary>
    /// Configures and maps the endpoints for the current handler 
    /// using the provided IEndpointRouteBuilder.
    /// This method is responsible for setting up the specific routes
    /// (e.g., HTTP GET, POST) for this request handler.
    /// </summary>
    /// <param name="endpoints">The IEndpointRouteBuilder used to define and map routes. <see cref="IEndpointRouteBuilder"/></param>
    /// <returns>
    /// A RouteHandlerBuilder object that represents the configured 
    /// route and can be used for further customization, such as adding filters.
    /// </returns>
    public RouteHandlerBuilder Configure(IEndpointRouteBuilder endpoints);
}