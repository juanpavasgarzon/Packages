namespace Pavas.Patterns.Context.Contracts;

public interface IContextProvider<TContext> where TContext : class
{
    public TContext? Context { get; set; }
}