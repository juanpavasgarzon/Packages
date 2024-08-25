using Pavas.Patterns.Result;

public class StockService
{
    private readonly List<Stock> _stocks =
    [
        new Stock
        {
            Name = "Computer",
            Quantity = 10,
            Price = 199
        },
        new Stock
        {
            Name = "Cellphone",
            Quantity = 10,
            Price = 199
        }
    ];

    public Result<Stock> ValidateStock(List<Stock> stocks)
    {
        var outStocks = _stocks.AsQueryable().
    }
}