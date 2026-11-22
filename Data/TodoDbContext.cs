using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Data;

// EF Core 的"账本"：所有 TodoItem 通过它读写 SQLite
public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> Todos => Set<TodoItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        // SQLite 单文件数据库，随应用走 —— Code-First：模型即表结构
        => options.UseSqlite("Data Source=todos.db");
}
