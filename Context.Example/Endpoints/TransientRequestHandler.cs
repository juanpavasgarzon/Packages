using Context.Example.Constants;
using Context.Example.ContextCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pavas.Patterns.Context.Contracts;

namespace Context.Example.Endpoints;

public static class TransientRequestHandler
{
    public static void MapTransientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(Resources.Contexts);

        group.MapGet(Resources.Transient, HandleTransient)
            .WithTags(Tags.Contexts)
            .Produces<TransientContext>(StatusCodes.Status200OK, "application/json")
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict, "application/problem+json");
    }

    private static Results<Ok<TransientContext>, Conflict<ProblemDetails>> HandleTransient(
        IContextProvider<TransientContext> contextProvider
    )
    {
        try
        {
            var context = contextProvider.Context ?? throw new NotFoundException("Context.Example Not Found");
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