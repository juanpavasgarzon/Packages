class Order
{
    public required string Consecutive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required List<Stock> Items { get; set; }
}