namespace LifeManagementApp.Services;

// 天气读数：温度 + 风速（单位 m/s —— 见实现里的注释）
public record WeatherReading(double Temperature, double WindSpeed);

// 契约：View 只问"现在风多大"，不关心数据从哪来
// —— 测试时可以换成 FakeWeatherService，这就是接口的意义
public interface IWeatherService
{
    Task<WeatherReading> GetCurrentAsync();
}
