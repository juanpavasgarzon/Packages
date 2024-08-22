using Context.ContextCases.Abstracts;
using Context.ContextCases.Contracts;

namespace Context.ContextCases;

public class TransientContext : ContextInitializer<TransientContext>, IContext
{
    public string TraceIdentifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid Guid { get; set; }
    public DateTime DateTime { get; set; }
}