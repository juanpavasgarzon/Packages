using Context.Example.ContextCases.Contracts;

namespace Context.Example.ContextCases.Tools;

public class ContextInitializer : IContextInitializer
{
    public TContext Initialize<TContext>() where TContext : class, IContext
    {
        var context = Activator.CreateInstance<TContext>();
        context.Name = typeof(TContext).Name;
        context.Guid = Guid.NewGuid();
        context.DateTime = DateTime.Now;
        return context;
    }
}