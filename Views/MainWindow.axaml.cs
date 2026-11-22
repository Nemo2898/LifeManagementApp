using Avalonia.Controls;
using Avalonia.Interactivity;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

// 首页：从 DI 容器拿依赖（构造注入），Top3 与天气都在 VM 里
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
        _ = _vm.RefreshAsync();          // 异步首刷，不堵 UI
    }

    // 打开待办管理页；窗口关闭后回来刷新 Top3
    private async void OpenTodos_Click(object? sender, RoutedEventArgs e)
    {
        var win = new TodosWindow(_repo);   // ← 传参：把 repo 递给下一页
        await win.ShowDialog(this);
        await _vm.RefreshAsync();
    }
}
