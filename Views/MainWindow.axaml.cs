using Avalonia.Controls;
using Avalonia.Interactivity;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

// Home page: dependencies come from the DI container (constructor injection);
// Top 3 and weather live in the ViewModel.
public partial class MainWindow : Window
{
    private readonly HomeViewModel _vm;
    private readonly ITodoRepository _repo;

    public MainWindow(ITodoRepository repo, IWeatherService weather)
    {
        InitializeComponent();
        _repo = repo;
        _vm = new HomeViewModel(repo, weather);
        DataContext = _vm;
        _ = _vm.RefreshAsync();          // async first refresh, does not block the UI
    }

    // Open the todos page; refresh Top 3 when that window closes
    private async void OpenTodos_Click(object? sender, RoutedEventArgs e)
    {
        var win = new TodosWindow(_repo);   // <- parameter passing: hand the repo to the next page
        await win.ShowDialog(this);
        await _vm.RefreshAsync();
    }
}
