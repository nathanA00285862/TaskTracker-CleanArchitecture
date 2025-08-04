using TaskTracker.Domain;
using Xunit;

namespace TaskTracker.Domain.Tests;

public class TaskItemTests
{
    [Fact]
    public void Constructor_Should_SetPropertiesCorrectly()
    {
        // Arrange
        var testId = 1;
        var testName = "Test Task";
        var testDate = DateTime.Now;

        // Act
        var task = new TaskItem(testId, testName, testDate);

        // Assert
        Assert.Equal(testId, task.Id);
        Assert.Equal(testName, task.Name);
        Assert.Equal(testDate, task.CreatedOn);
        Assert.False(task.IsCompleted);
    }

    [Fact]
    public void IsCompleted_Should_BeMutable()
    {
        // Arrange
        var task = new TaskItem(1, "Test", DateTime.Now);

        // Act
        task.IsCompleted = true;

        // Assert
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public void NonCompletionProperties_Should_BeReadOnly()
    {
        // Arrange
        var task = new TaskItem(1, "Original", DateTime.Now);

        // Get property info with explicit null checks
        var idProp = task.GetType().GetProperty("Id") ??
            throw new InvalidOperationException("Id property not found");
        var nameProp = task.GetType().GetProperty("Name") ??
            throw new InvalidOperationException("Name property not found");
        var dateProp = task.GetType().GetProperty("CreatedOn") ??
            throw new InvalidOperationException("CreatedOn property not found");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => idProp.SetValue(task, 2));
        Assert.Throws<ArgumentException>(() => nameProp.SetValue(task, "Modified"));
        Assert.Throws<ArgumentException>(() => dateProp.SetValue(task, DateTime.MinValue));
    }

    // Bonus test for additional coverage
    [Fact]
    public void Properties_Should_HaveCorrectValuesAfterConstruction()
    {
        // Arrange & Act
        var task = new TaskItem(42, "Important Task", DateTime.Parse("2023-01-01"));

        // Assert
        Assert.Equal(42, task.Id);
        Assert.Equal("Important Task", task.Name);
        Assert.Equal(DateTime.Parse("2023-01-01"), task.CreatedOn);
        Assert.False(task.IsCompleted);
    }
}