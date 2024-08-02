using Pavas.Patterns.Example.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", WeatherForecastHttpContext.Handle)
    .WithTags(nameof(WeatherForecast))
    .WithName(nameof(WeatherForecast))
    .WithOpenApi();

app.Run();