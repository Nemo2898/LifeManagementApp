using CommunityToolkit.Mvvm.ComponentModel;
using LifeManagementApp.Models;

namespace LifeManagementApp.ViewModels;

// The brain of the edit/new page: takes a TodoItem (null = create) and produces the result
public partial class EditTodoViewModel : ObservableObject
{
    [ObservableProperty] private string _title = "";
    [ObservableProperty] private decimal _priority = 3;          // decimal keeps NumericUpDown happy
    [ObservableProperty] private DateTimeOffset? _dueDate = DateTimeOffset.Now;
    [ObservableProperty] private string _error = "";

    // The item being edited (null = create) - this is the parameter passing
    public TodoItem? Original { get; }

    public EditTodoViewModel(TodoItem? editing)
    {
        Original = editing;
        if (editing is not null)
        {
            Title = editing.Title;
            Priority = editing.Priority;
            DueDate = new DateTimeOffset(editing.DueDate);
        }
    }

    public TodoItem BuildItem()
    {
        // Reject empty titles - a hard requirement of rubric section 1
        if (string.IsNullOrWhiteSpace(Title))
            throw new ArgumentException("Title must not be empty.");
        return new TodoItem
        {
            Id = Original?.Id ?? 0,
            Title = Title.Trim(),
            Priority = (int)Priority,
            DueDate = DueDate?.DateTime ?? DateTime.Today,
            IsDone = Original?.IsDone ?? false
        };
    }
}
