using Avalonia.Controls;
using Avalonia.Interactivity;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

// Edit/new window: constructed with (repo, editing) - editing == null means create
public partial class EditTodoWindow : Window
{
    private readonly ITodoRepository _repo;
    private readonly EditTodoViewModel _vm;

    public EditTodoWindow(ITodoRepository repo, TodoItem? editing)
    {
        InitializeComponent();
        _repo = repo;
        _vm = new EditTodoViewModel(editing);
        DataContext = _vm;
        Title = editing is null ? "New todo" : "Edit todo";
    }

    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var item = _vm.BuildItem();          // empty title is rejected here
            if (_vm.Original is null) await _repo.AddAsync(item);
            else await _repo.UpdateAsync(item);
            Close();
        }
        catch (ArgumentException ex)
        {
            _vm.Error = ex.Message;              // red hint, keep the window open
        }
    }
}
