using Microsoft.EntityFrameworkCore;
using ToDoTask.Models;

namespace ToDoTask.Services;

public class DB : DbContext
{
    public DbSet<TaskItem> TaskItems { get; set; }

    public DB()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=Todo.db");
    }
}