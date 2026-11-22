using Avalonia.Controls;
using Avalonia.Interactivity;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

// 待办管理页：列表 + 增/改/完成
public partial class TodosWindow : Window
{
    private readonly ITodoRepository _repo;
    private readonly TodosViewModel _vm = new();

    public TodosWindow(ITodoRepository repo)
    {
        InitializeComponent();
        _repo = repo;
        DataContext = _vm;
        Loaded += async (_, _) => await RefreshAsync();
    }

    private async Task RefreshAsync()
        => _vm.Reload(await _repo.GetAllAsync());

    // 挑中哪条就用哪条 —— 没选中就提示
    private TodoItem? Selected => ItemsList.SelectedItem as TodoItem;

    private async void Add_Click(object? sender, RoutedEventArgs e)
    {
        await new EditTodoWindow(_repo, null).ShowDialog(this);   // null = 新增
        await RefreshAsync();
    }

    private async void Edit_Click(object? sender, RoutedEventArgs e)
    {
        if (Selected is null) { _vm.Error = "Select an item first."; return; }
        await new EditTodoWindow(_repo, Selected).ShowDialog(this);  // ← 传参：把整条待办递过去
        await RefreshAsync();
    }

    private async void Done_Click(object? sender, RoutedEventArgs e)
    {
        if (Selected is null) { _vm.Error = "Select an item first."; return; }
        Selected.IsDone = true;
        await _repo.UpdateAsync(Selected);
        await RefreshAsync();
    }
}
