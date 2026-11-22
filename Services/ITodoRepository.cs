using LifeManagementApp.Models;

namespace LifeManagementApp.Services;

// Contract: whoever stores todos for me must honor this sheet - the UI layer
// does not care whether the storage is SQLite or an in-memory list.
public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync();
    Task AddAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
}
