using LifeManagementApp.Models;

namespace LifeManagementApp.Services;

// 契约：谁替我存待办，只认这张纸 —— 界面层不关心底层是 SQLite 还是内存
public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync();
    Task AddAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
}
