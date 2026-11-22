using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Data;

// The EF Core ledger: all TodoItems are read/written through it into SQLite
public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> Todos => Set<TodoItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        // Single-file SQLite database next to the app - Code-First: the model is the schema
        => options.UseSqlite("Data Source=todos.db");
}
