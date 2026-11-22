using Avalonia.Controls;
using Avalonia.Interactivity;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

// Todos page: list + add / edit / mark-done
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

    // Whichever row is selected is used - hint if nothing is selected
    private TodoItem? Selected => ItemsList.SelectedItem as TodoItem;

    private async void Add_Click(object? sender, RoutedEventArgs e)
    {
        await new EditTodoWindow(_repo, null).ShowDialog(this);   // null = create
        await RefreshAsync();
    }

    private async void Edit_Click(object? sender, RoutedEventArgs e)
    {
        if (Selected is null) { _vm.Error = "Select an item first."; return; }
        await new EditTodoWindow(_repo, Selected).ShowDialog(this);  // <- parameter passing: hand the whole todo over
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
