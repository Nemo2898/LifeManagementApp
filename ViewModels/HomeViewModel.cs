using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

// 首页的脑：天气 + Top3 —— 所有数据都从这里流向界面
public partial class HomeViewModel : ObservableObject
{
    private readonly ITodoRepository _repo;
    private readonly IWeatherService _weather;

    [ObservableProperty] private string _weatherInfo = "Loading weather…";
    [ObservableProperty] private bool _isKiteTime;       // 风速 > 5 → 弹风筝提醒

    public ObservableCollection<TodoItem> TopTodos { get; } = new();

    public HomeViewModel(ITodoRepository repo, IWeatherService weather)
    {
        _repo = repo;
        _weather = weather;
    }

    // 每次回到首页调用：刷新天气 + 重算 Top3
    public async Task RefreshAsync()
    {
        // —— 天气：断网不炸，给兜底文案 ——//
        try
        {
            var w = await _weather.GetCurrentAsync();
            WeatherInfo = $"{w.Temperature:F1} °C — wind {w.WindSpeed:F1} m/s";
            IsKiteTime = w.WindSpeed > 5.0;              // 评分表 §1 的风筝线
        }
        catch
        {
            WeatherInfo = "Weather unavailable (offline?)";
            IsKiteTime = false;
        }

        // —— Top3：未完成 → 优先级升序 → 同级按截止日早 ——//
        TopTodos.Clear();
        var all = await _repo.GetAllAsync();
        foreach (var t in all.Where(t => !t.IsDone)
                             .OrderBy(t => t.Priority)
                             .ThenBy(t => t.DueDate)
                             .Take(3))
            TopTodos.Add(t);
    }
}
