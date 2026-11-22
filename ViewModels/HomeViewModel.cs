using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

// The brain of the home page: weather + Top 3 - all data flows to the UI from here
public partial class HomeViewModel : ObservableObject
{
    private readonly ITodoRepository _repo;
    private readonly IWeatherService _weather;

    [ObservableProperty] private string _weatherInfo = "Loading weather…";
    [ObservableProperty] private bool _isKiteTime;       // wind > 5 -> show the kite banner

    public ObservableCollection<TodoItem> TopTodos { get; } = new();

    public HomeViewModel(ITodoRepository repo, IWeatherService weather)
    {
        _repo = repo;
        _weather = weather;
    }

    // Called every time the home page is shown: refresh weather + recompute Top 3
    public async Task RefreshAsync()
    {
        // -- Weather: no crash when offline, show a fallback text --
        try
        {
            var w = await _weather.GetCurrentAsync();
            WeatherInfo = $"{w.Temperature:F1} °C — wind {w.WindSpeed:F1} m/s";
            IsKiteTime = w.WindSpeed > 5.0;              // the kite threshold from rubric section 1
        }
        catch
        {
            WeatherInfo = "Weather unavailable (offline?)";
            IsKiteTime = false;
        }

        // -- Top 3: open items -> priority ascending -> same priority by earliest due date --
        TopTodos.Clear();
        var all = await _repo.GetAllAsync();
        foreach (var t in all.Where(t => !t.IsDone)
                             .OrderBy(t => t.Priority)
                             .ThenBy(t => t.DueDate)
                             .Take(3))
            TopTodos.Add(t);
    }
}
