using LifeManagementApp.Data;
using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Services;

// 契约的履约人：把增删改查翻译成 EF 的数据库操作
public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _db = new();

    public TodoRepository()
    {
        // 没有库就建库（Code-First 的极简版；生产应使用迁移）
        _db.Database.EnsureCreated();
    }

    public async Task<List<TodoItem>> GetAllAsync()
        => await _db.Todos.OrderBy(t => t.Priority).ToListAsync();

    public async Task AddAsync(TodoItem item)
    {
        _db.Todos.Add(item);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem item)
    {
        _db.Todos.Update(item);       // 已跟踪实体的状态同步
        await _db.SaveChangesAsync();
    }
}
