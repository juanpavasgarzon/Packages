using Context.Example.ContextCases;
using Pavas.Patterns.Context.Contracts;

namespace Context.Example;

public class ScopedContextMiddleware(IContextFactory<ScopedContext> scopedFactory) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var scopedContext = ScopedContext.Initialize();
        scopedContext.TraceIdentifier = context.TraceIdentifier;
        scopedFactory.Construct(scopedContext);
        await next(context);
        scopedFactory.Destruct();
    }
}