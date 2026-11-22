using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LifeManagementApp.Models;

namespace LifeManagementApp.ViewModels;

// 待办管理页的脑：持有全部条目，列表变化自动刷新界面
public partial class TodosViewModel : ObservableObject
{
    [ObservableProperty] private string _error = "";

    public ObservableCollection<TodoItem> Todos { get; } = new();

    public void Reload(IEnumerable<TodoItem> items)
    {
        Todos.Clear();
        foreach (var t in items) Todos.Add(t);
    }
}
