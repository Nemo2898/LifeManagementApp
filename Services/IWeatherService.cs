namespace LifeManagementApp.Services;

// A weather reading: temperature + wind speed (unit: m/s - see the implementation)
public record WeatherReading(double Temperature, double WindSpeed);

// Contract: the View only asks "how windy is it now" and does not care where data comes from.
// - In tests this can be swapped for FakeWeatherService; that is the point of the interface.
public interface IWeatherService
{
    Task<WeatherReading> GetCurrentAsync();
}
