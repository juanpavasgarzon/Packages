using Context.Example.ContextCases.Abstracts;
using Context.Example.ContextCases.Contracts;

namespace Context.Example.ContextCases;

public class ScopedContext : ContextInitializer<ScopedContext>, IContext
{
    public string TraceIdentifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid Guid { get; set; }
    public DateTime DateTime { get; set; }
}