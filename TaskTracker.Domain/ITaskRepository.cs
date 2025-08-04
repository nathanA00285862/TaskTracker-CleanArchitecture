namespace TaskTracker.Domain;

public interface ITaskRepository
{
    void Add(TaskItem task);
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    void Update(TaskItem task);
}