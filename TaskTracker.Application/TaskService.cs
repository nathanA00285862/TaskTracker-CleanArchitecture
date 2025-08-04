using TaskTracker.Domain;

namespace TaskTracker.Application;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void AddTask(string name)
    {
        var nextId = _repository.GetAll().Count() + 1;
        var task = new TaskItem(nextId, name, DateTime.Now);
        _repository.Add(task);
    }

    public IEnumerable<TaskItem> ListTasks() => _repository.GetAll();

    public void CompleteTask(int taskId)
    {
        var task = _repository.GetById(taskId);
        if (task != null)
        {
            task.IsCompleted = true;
            _repository.Update(task);
        }
    }
}