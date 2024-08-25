using Pavas.Patterns.Result;
using Pavas.Patterns.Result.Errors;

namespace Result.Example;

public class StockService(List<Stock> stocks)
{
    public Result<List<Stock>> ValidateStock(List<Stock> stocks)
    {
        var invalidStock = stocks.Find(stock =>
        {
            var existingStock = stocks.Find(element => element.Name == stock.Name);
            return existingStock == null || existingStock.Quantity < stock.Quantity;
        });

        if (invalidStock == null)
            return Result<List<Stock>>.Success(stocks);

        var error = ErrorFactory.CreateSystemError("Invalid", "Invalid", "Stocks are invalid");
        return Result<List<Stock>>.Failure(error);
    }
}