using Context.ContextCases;
using Pavas.Patterns.Context.Contracts;

namespace Context;

public class TransientContextMiddleware(IContextFactory<TransientContext> transientFactory) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var transientContext = TransientContext.Initialize();
        transientContext.TraceIdentifier = context.TraceIdentifier;
        transientFactory.Construct(transientContext);
        await next(context);
        transientFactory.Destruct();
    }
}