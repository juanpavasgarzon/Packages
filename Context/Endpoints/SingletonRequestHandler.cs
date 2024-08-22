using Context.Constants;
using Context.ContextCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pavas.Patterns.Context.Contracts;

namespace Context.Endpoints;

public static class GetSingletonRequestHandler
{
    public static void MapSingletonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(Resources.Contexts);

        group.MapGet(Resources.Singleton, HandleSingleton)
            .WithTags(Tags.Contexts)
            .Produces<SingletonContext>(StatusCodes.Status200OK, "application/json")
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict, "application/problem+json");
    }

    private static Results<Ok<SingletonContext>, Conflict<ProblemDetails>> HandleSingleton(
        IContextProvider<SingletonContext> contextProvider
    )
    {
        try
        {
            var context = contextProvider.Context ?? throw new NotFoundException("Context Not Found");
            return TypedResults.Ok(context);
        }
        catch (Exception e)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Server error",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                Extensions = { ["message"] = e.Message },
                Status = StatusCodes.Status409Conflict
            };

            return TypedResults.Conflict(problemDetails);
        }
    }
}