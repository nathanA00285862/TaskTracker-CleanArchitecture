using TaskTracker.Domain;

namespace TaskTracker.Application;

public interface ITaskService
{
    void AddTask(string name);
    IEnumerable<TaskItem> ListTasks();
    void CompleteTask(int taskId);
}