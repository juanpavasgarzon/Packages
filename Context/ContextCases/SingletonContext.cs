using Context.ContextCases.Abstracts;
using Context.ContextCases.Contracts;

namespace Context.ContextCases;

public class SingletonContext : ContextInitializer<SingletonContext>, IContext
{
    public string Name { get; set; } = string.Empty;
    public Guid Guid { get; set; }
    public DateTime DateTime { get; set; }
}