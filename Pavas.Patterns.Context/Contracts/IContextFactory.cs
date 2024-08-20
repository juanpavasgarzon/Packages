namespace Pavas.Patterns.Context.Contracts;

public interface IContextFactory<in TContext> where TContext : class
{
    public void Construct(TContext context);
    public void Destruct();
}