using TaskTracker.Domain;

namespace TaskTracker.Infrastructure;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public void Add(TaskItem task) => _tasks.Add(task);

    public IEnumerable<TaskItem> GetAll() => _tasks;

    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void Update(TaskItem task)
    {
        // In memory implementation doesn't need special update logic
    }
}