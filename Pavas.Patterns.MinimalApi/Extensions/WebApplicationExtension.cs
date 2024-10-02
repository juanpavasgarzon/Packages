using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.MinimalApi.Contracts;

namespace Pavas.Patterns.MinimalApi.Extensions;

/// <summary>
/// Provides extension methods for mapping request handlers in WebApplication instances.
/// </summary>
public static class WebApplicationExtension
{
    /// <summary>
    /// Maps all the endpoints defined in IRequestHandler implementations registered in the service container.
    /// This method processes each IRequestHandler, allowing them to configure their own endpoints within the application.
    /// No global options or filters are applied to the endpoints.
    /// </summary>
    /// <typeparam name="TApp">The application type that implements IApplicationBuilder and IEndpointRouteBuilder.</typeparam>
    /// <param name="app">The current WebApplication instance where the endpoints will be mapped.</param>
    public static void MapRequestHandlers<TApp>(this TApp app)
        where TApp : class, IApplicationBuilder, IEndpointRouteBuilder
    {
        using var scope = app.ApplicationServices.CreateScope();
        var requestHandlers = scope.ServiceProvider.GetServices<IRequestHandler>();
        foreach (var requestHandler in requestHandlers)
        {
            requestHandler.Configure(app);
        }
    }

    /// <summary>
    /// Maps all the endpoints defined in IRequestHandler implementations registered in the service container
    /// and applies global configuration options (such as filters) to each mapped endpoint.
    /// </summary>
    /// <typeparam name="TApp">The application type that implements IApplicationBuilder and IEndpointRouteBuilder.</typeparam>
    /// <param name="app">The current WebApplication instance where the endpoints will be mapped.</param>
    /// <param name="globalOptions">A delegate to configure global options like filters on the RouteHandlerBuilder for each endpoint.</param>
    public static void MapRequestHandlers<TApp>(this TApp app, Action<RouteHandlerBuilder> globalOptions)
        where TApp : class, IApplicationBuilder, IEndpointRouteBuilder
    {
        using var scope = app.ApplicationServices.CreateScope();
        var requestHandlers = scope.ServiceProvider.GetServices<IRequestHandler>();
        foreach (var requestHandler in requestHandlers)
        {
            var builder = requestHandler.Configure(app);
            globalOptions.Invoke(builder);
        }
    }
}