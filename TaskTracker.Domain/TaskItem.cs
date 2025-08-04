namespace TaskTracker.Domain;

public class TaskItem
{
    public int Id { get; }
    public string Name { get; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedOn { get; }

    public TaskItem(int id, string name, DateTime createdOn)
    {
        Id = id;
        Name = name;
        CreatedOn = createdOn;
        IsCompleted = false;
    }
}