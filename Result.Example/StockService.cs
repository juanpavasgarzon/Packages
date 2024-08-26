using Pavas.Patterns.Result;
using Pavas.Patterns.Result.Errors;

namespace Result.Example;

public class StockService(List<Stock> stocks)
{
    public List<Stock> Stocks { get; private init; } = stocks;

    public Result<List<Stock>> ValidateStock(List<Stock> stocks)
    {
        var invalidStock = stocks.Find(stock =>
        {
            var existingStock = Stocks.Find(element => element.Name == stock.Name);
            return existingStock == null || existingStock.Quantity < stock.Quantity;
        });

        if (invalidStock == null)
            return Result<List<Stock>>.Success(stocks);

        var error = ErrorFactory.CreateSystemError("Invalid", "Invalid", "Stocks are invalid");
        return Result<List<Stock>>.Failure(error);
    }

    public Result<List<Stock>> UpdateStock(List<Stock> stocks)
    {
        foreach (var stock in stocks)
        {
            var existingStock = Stocks.Find(element => element.Name == stock.Name);
            existingStock!.Quantity -= stock.Quantity;
        }

        throw new Exception("Example exception");

        return Result<List<Stock>>.Success(stocks);
    }
}