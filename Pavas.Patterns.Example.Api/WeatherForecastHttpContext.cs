namespace Pavas.Patterns.Example.Api;

public static class WeatherForecastHttpContext
{
    public static WeatherForecast[] Handle()
    {
        return Enumerable.Range(1, 10)
            .Select(WeatherForecast.RandomWeatherForecast)
            .ToArray();
    }
}