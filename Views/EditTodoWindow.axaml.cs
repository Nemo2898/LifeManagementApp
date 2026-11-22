using Avalonia.Controls;
using Avalonia.Interactivity;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;

namespace LifeManagementApp.Views;

// 编辑/新增窗：构造时收 (repo, editing) —— editing 为 null 就是新增
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
            var item = _vm.BuildItem();          // 空标题在这里被拒
            if (_vm.Original is null) await _repo.AddAsync(item);
            else await _repo.UpdateAsync(item);
            Close();
        }
        catch (ArgumentException ex)
        {
            _vm.Error = ex.Message;              // 红字提示，不关窗
        }
    }
}
