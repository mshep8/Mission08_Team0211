namespace Mission08_Team0211.Models;

public class EFQuadrantsRepository : IQuadrantsRepository
{
    private readonly QuadrantsContext _context;

    public EFQuadrantsRepository(QuadrantsContext context)
    {
        _context = context;
    }

    public IQueryable<ToDoTask> ToDoTasks => _context.ToDoTasks;
    public IQueryable<Category> Categories => _context.Categories;

    public void AddTask(ToDoTask task)
    {
        NormalizeTaskDates(task);

        _context.ToDoTasks.Add(task);
        _context.SaveChanges();
    }

    public void UpdateTask(ToDoTask task)
    {
        NormalizeTaskDates(task);

        _context.ToDoTasks.Update(task);
        _context.SaveChanges();
    }

    public void DeleteTask(ToDoTask task)
    {
        _context.ToDoTasks.Remove(task);
        _context.SaveChanges();
    }

    private void NormalizeTaskDates(ToDoTask task)
    {
        if (task?.DueDate == null)
        {
            return;
        }

        var dt = task.DueDate.Value;

        if (dt.Kind == DateTimeKind.Utc)
        {
            // already correct
            task.DueDate = dt;
            return;
        }

        if (dt.Kind == DateTimeKind.Unspecified)
        {
            // Treat unspecified values as UTC to satisfy Npgsql's timestamptz requirement.
            task.DueDate = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }
        else
        {
            // Local -> convert to UTC
            task.DueDate = dt.ToUniversalTime();
        }
    }
}
