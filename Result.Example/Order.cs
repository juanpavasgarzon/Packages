namespace Result.Example;

public class Order
{
    public required string Consecutive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public List<Stock> Items { get; set; } = [];
}