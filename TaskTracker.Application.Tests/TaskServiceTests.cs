using Moq;
using TaskTracker.Application;
using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Application.Tests;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockRepo;
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        _mockRepo = new Mock<ITaskRepository>();
        _service = new TaskService(_mockRepo.Object);
    }

    // ===== AddTask Tests =====
    [Fact]
    public void AddTask_Should_GenerateSequentialIds()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAll())
                .Returns(new[] { new TaskItem(1, "Existing", DateTime.Now) });

        // Act
        _service.AddTask("New Task");

        // Assert
        _mockRepo.Verify(r => r.Add(It.Is<TaskItem>(t => t.Id == 2)));
    }

    [Fact]
    public void AddTask_Should_UseCurrentDateTime()
    {
        // Arrange
        var testStart = DateTime.Now;

        // Act
        _service.AddTask("New Task");

        // Assert
        _mockRepo.Verify(r => r.Add(It.Is<TaskItem>(t =>
            t.CreatedOn >= testStart && t.CreatedOn <= DateTime.Now)));
    }

    [Fact]
    public void AddTask_Should_HandleEmptyName()
    {
        // Act & Assert (should not throw)
        _service.AddTask("");
        _mockRepo.Verify(r => r.Add(It.IsAny<TaskItem>()));
    }

    // ===== ListTasks Tests =====
    [Fact]
    public void ListTasks_Should_ReturnAllTasks()
    {
        // Arrange
        var expectedTasks = new[]
        {
            new TaskItem(1, "Task 1", DateTime.Now),
            new TaskItem(2, "Task 2", DateTime.Now)
        };
        _mockRepo.Setup(r => r.GetAll()).Returns(expectedTasks);

        // Act
        var result = _service.ListTasks();

        // Assert
        Assert.Equal(expectedTasks, result);
    }

    [Fact]
    public void ListTasks_Should_ReturnEmptyForNoTasks()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAll()).Returns(Enumerable.Empty<TaskItem>());

        // Act
        var result = _service.ListTasks();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ListTasks_Should_NotModifyRepositoryData()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAll()).Returns(new[] { new TaskItem(1, "Test", DateTime.Now) });

        // Act
        _service.ListTasks();

        // Assert
        _mockRepo.Verify(r => r.GetAll(), Times.Once);
        _mockRepo.VerifyNoOtherCalls();
    }

    // ===== CompleteTask Tests =====
    [Fact]
    public void CompleteTask_Should_UpdateExistingTask()
    {
        // Arrange
        var testTask = new TaskItem(1, "Test", DateTime.Now);
        _mockRepo.Setup(r => r.GetById(1)).Returns(testTask);

        // Act
        _service.CompleteTask(1);

        // Assert
        Assert.True(testTask.IsCompleted);
        _mockRepo.Verify(r => r.Update(testTask), Times.Once);
    }

    [Fact]
    public void CompleteTask_Should_NotUpdateMissingTask()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((TaskItem?)null);

        // Act
        _service.CompleteTask(999);

        // Assert
        _mockRepo.Verify(r => r.Update(It.IsAny<TaskItem>()), Times.Never);
    }

    [Fact]
    public void CompleteTask_Should_HandleAlreadyCompleted()
    {
        // Arrange
        var testTask = new TaskItem(1, "Test", DateTime.Now) { IsCompleted = true };
        _mockRepo.Setup(r => r.GetById(1)).Returns(testTask);

        // Act
        _service.CompleteTask(1);

        // Assert (should still call update even if already completed)
        _mockRepo.Verify(r => r.Update(testTask), Times.Once);
    }
}