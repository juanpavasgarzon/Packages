namespace Context.Cases.Contracts;

public interface IContext
{
    public string Name { get; set; }
    public Guid Guid { get; set; }
    public DateTime DateTime { get; set; }
}