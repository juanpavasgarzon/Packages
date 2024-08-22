using Context.Example.ContextCases.Contracts;

namespace Context.Example.ContextCases;

public class SingletonContext : IContext
{
    public string Name { get; set; } = string.Empty;
    public Guid Guid { get; set; }
    public DateTime DateTime { get; set; }
}