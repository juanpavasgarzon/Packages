using Context.Cases.Abstracts;
using Context.Cases.Contracts;

namespace Context.Cases;

public class ScopedContext : ContextInitializer<ScopedContext>, IContext
{
    public string TraceIdentifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid Guid { get; set; }
    public DateTime DateTime { get; set; }
}