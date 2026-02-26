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
}
