using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LifeManagementApp.Models;

namespace LifeManagementApp.ViewModels;

// The brain of the todos page: holds all items; list changes refresh the UI automatically
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
