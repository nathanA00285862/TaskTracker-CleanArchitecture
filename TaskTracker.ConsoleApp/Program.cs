using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application;    // For ITaskService
using TaskTracker.Domain;        // For ITaskRepository
using TaskTracker.Infrastructure; // For InMemoryTaskRepository

// Set up our tools (Dependency Injection)
var services = new ServiceCollection();
services.AddScoped<ITaskRepository, InMemoryTaskRepository>();
services.AddScoped<ITaskService, TaskService>();
var serviceProvider = services.BuildServiceProvider();

// Get our task service ready
var taskService = serviceProvider.GetRequiredService<ITaskService>();

// Simple menu system
while (true)
{
    Console.Clear();
    Console.WriteLine("MY TASK LIST");
    Console.WriteLine("1. View All Tasks");
    Console.WriteLine("2. Add New Task");
    Console.WriteLine("3. Complete Task");
    Console.WriteLine("4. Exit");
    Console.Write("Choose (1-4): ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": // View Tasks
            Console.WriteLine("\nYOUR TASKS:");
            foreach (var task in taskService.ListTasks())
            {
                Console.WriteLine($"{task.Id}. {task.Name} - {(task.IsCompleted ? "DONE" : "Pending")}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            break;

        case "2": // Add Task
            Console.Write("\nEnter task name: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                taskService.AddTask(name);
                Console.WriteLine("Task added!");
            }
            Thread.Sleep(1000);
            break;

            // Add cases 3 and 4 here
    }
}