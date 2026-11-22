using System.Net.Http;
using System.Text.Json;

namespace LifeManagementApp.Services;

// The contract implementer: the real Open-Meteo weather API
public class OpenMeteoWeatherService : IWeatherService
{
    private static readonly HttpClient Http = new();

    // Vaasa coordinates (where the school is).
    // The teacher's trap: Open-Meteo defaults to km/h for wind speed;
    // we must pass wind_speed_unit=ms explicitly, otherwise the 5 m/s check is wrong.
    private const string Url =
        "https://api.open-meteo.com/v1/forecast?latitude=63.10&longitude=21.61" +
        "&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";

    public async Task<WeatherReading> GetCurrentAsync()
    {
        var json = await Http.GetStringAsync(Url);          // async fetch, does not block the UI
        var current = JsonDocument.Parse(json).RootElement.GetProperty("current");

        return new WeatherReading(
            current.GetProperty("temperature_2m").GetDouble(),
            current.GetProperty("wind_speed_10m").GetDouble());
    }
}
