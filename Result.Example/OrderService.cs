namespace Result.Example;

public class OrderService(Order order)
{
    public Order Order { get; private init; } = order;

    public Pavas.Patterns.Result.Result<string> SendNotification()
    {
        return Pavas.Patterns.Result.Result<string>.Success($"Order {Order.Consecutive} Is Success");
    }
}