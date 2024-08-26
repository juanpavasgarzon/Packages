using Pavas.Patterns.Result;
using Pavas.Patterns.Result.Errors;
using Result.Example;

List<Stock> mockStocks =
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
    },
];

var mockOrder = new Order
{
    Consecutive = "0000001",
    CreatedAt = DateTime.UtcNow,
    Items =
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
    ]
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(mockStocks);
builder.Services.AddSingleton(mockOrder);
builder.Services.AddSingleton<StockService>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var serviceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(builder.Services);
var orderService = serviceProvider.GetRequiredService<OrderService>();
var stockService = serviceProvider.GetRequiredService<StockService>();
var order = serviceProvider.GetRequiredService<Order>();

var result = Pavas.Patterns.Result.Result.Success()
    .Bind(() => stockService.ValidateStock(order.Items))
    .TryCatch(_ => stockService.UpdateStock(order.Items), ErrorFactory.FromException)
    .Bind(_ => orderService.SendNotification())
    .ThenSuccess(Console.WriteLine)
    .ThenFailure(error => Console.WriteLine(error.ToString()))
    .Match(_ => true, _ => false);

Console.WriteLine($"Is Ok: {result}");

app.UseHttpsRedirection();

app.Run();