using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0211.Models;

public class QuadrantsContext : DbContext
{
    public QuadrantsContext(DbContextOptions<QuadrantsContext> options) : base(options)
    {
    }

    public DbSet<ToDoTask> ToDoTasks => Set<ToDoTask>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.CategoryName)
            .IsUnique();

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Home" },
            new Category { CategoryId = 2, CategoryName = "School" },
            new Category { CategoryId = 3, CategoryName = "Work" },
            new Category { CategoryId = 4, CategoryName = "Church" }
        );

        modelBuilder.Entity<ToDoTask>().HasData(
            new ToDoTask
            {
                ToDoTaskId = 1,
                Task = "Finish Mission 08 setup",
                DueDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                Quadrant = 1,
                CategoryId = 2,
                Completed = false
            },
            new ToDoTask
            {
                ToDoTaskId = 2,
                Task = "Plan weekly schedule",
                DueDate = new DateTime(2026, 2, 7, 0, 0, 0, DateTimeKind.Utc),
                Quadrant = 2,
                CategoryId = 1,
                Completed = false
            }
        );
    }
}
