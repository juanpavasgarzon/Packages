using Context.Example.ContextCases;
using Context.Example.ContextCases.Contracts;
using Pavas.Patterns.Context.Contracts;

namespace Context.Example.Middlewares;

public class ScopedContextMiddleware(
    IContextFactory<ScopedContext> scopedFactory,
    IContextInitializer contextInitializer
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var scopedContext = contextInitializer.Initialize<ScopedContext>();
        scopedContext.TraceIdentifier = context.TraceIdentifier;
        scopedFactory.Construct(scopedContext);
        await next(context);
        scopedFactory.Destruct();
    }
}