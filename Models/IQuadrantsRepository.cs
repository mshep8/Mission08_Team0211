namespace Mission08_Team0211.Models;

public interface IQuadrantsRepository
{
    IQueryable<ToDoTask> ToDoTasks { get; }
    IQueryable<Category> Categories { get; }

    void AddTask(ToDoTask task);
    void UpdateTask(ToDoTask task);
    void DeleteTask(ToDoTask task);
}
