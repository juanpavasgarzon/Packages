using Context.Example.ContextCases;
using Context.Example.ContextCases.Contracts;
using Pavas.Patterns.Context.Contracts;

namespace Context.Example.Middlewares;

public class TransientContextMiddleware(
    IContextFactory<TransientContext> transientFactory,
    IContextInitializer contextInitializer
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var transientContext = contextInitializer.Initialize<TransientContext>();
        transientContext.TraceIdentifier = context.TraceIdentifier;
        transientFactory.Construct(transientContext);
        await next(context);
        transientFactory.Destruct();
    }
}