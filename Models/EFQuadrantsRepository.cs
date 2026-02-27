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
        _context.ToDoTasks.Add(task);
        _context.SaveChanges();
    }

    public void UpdateTask(ToDoTask task)
    {
        _context.ToDoTasks.Update(task);
        _context.SaveChanges();
    }

    public void DeleteTask(ToDoTask task)
    {
        _context.ToDoTasks.Remove(task);
        _context.SaveChanges();
    }
}
