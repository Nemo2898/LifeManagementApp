using System.Net.Http;
using System.Text.Json;

namespace LifeManagementApp.Services;

// 履约人：Open-Meteo 真实天气 API
public class OpenMeteoWeatherService : IWeatherService
{
    private static readonly HttpClient Http = new();

    // Vaasa 坐标（学校所在处）。
    // ⚠️ 老师埋的坑：Open-Meteo 默认风速单位是 km/h，
    // 必须显式传 wind_speed_unit=ms，否则 5 m/s 的判断全错。
    private const string Url =
        "https://api.open-meteo.com/v1/forecast?latitude=63.10&longitude=21.61" +
        "&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";

    public async Task<WeatherReading> GetCurrentAsync()
    {
        var json = await Http.GetStringAsync(Url);          // 异步拉取，不阻塞 UI
        var current = JsonDocument.Parse(json).RootElement.GetProperty("current");

        return new WeatherReading(
            current.GetProperty("temperature_2m").GetDouble(),
            current.GetProperty("wind_speed_10m").GetDouble());
    }
}
