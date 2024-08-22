using Context.Cases.Abstracts;
using Context.Cases.Contracts;

namespace Context.Cases;

public class SingletonContext : ContextInitializer<SingletonContext>, IContext
{
    public string Name { get; set; } = string.Empty;
    public Guid Guid { get; set; }
    public DateTime DateTime { get; set; }
}