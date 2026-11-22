using CommunityToolkit.Mvvm.ComponentModel;
using LifeManagementApp.Models;

namespace LifeManagementApp.ViewModels;

// 编辑/新增页的脑：收一个 TodoItem（null = 新建），吐出编辑结果
public partial class EditTodoViewModel : ObservableObject
{
    [ObservableProperty] private string _title = "";
    [ObservableProperty] private decimal _priority = 3;          // NumericUpDown 用 decimal 最稳
    [ObservableProperty] private DateTimeOffset? _dueDate = DateTimeOffset.Now;
    [ObservableProperty] private string _error = "";

    // 编辑的对象（null = 新增）—— 这就是"传参"
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
        // 空标题拒绝 —— 评分表 §1 的硬指标
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
