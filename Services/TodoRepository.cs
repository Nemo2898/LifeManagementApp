using LifeManagementApp.Data;
using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Services;

// The contract implementer: translates CRUD into EF database operations
public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _db = new();

    public TodoRepository()
    {
        // Create the database if missing (the minimalist Code-First; production would use migrations)
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
        _db.Todos.Update(item);       // sync the state of the tracked entity
        await _db.SaveChangesAsync();
    }
}
