namespace Context.Example.ContextCases.Contracts;

public interface IContextInitializer
{
    public TContext Initialize<TContext>() where TContext : class, IContext;
}