# Task Tracker
A simple console-based task management application built using Clean Architecture principles.
## Architecture
The solution follows Clean Architecture with four projects:
- **TaskTracker.Domain**: Core entities and interfaces
- **TaskTracker.Application**: Business logic and use cases
- **TaskTracker.Infrastructure**: Data persistence implementation
- **TaskTracker.ConsoleApp**: User interface and interaction
## Features
- Add new tasks
- View all tasks
- Mark tasks as complete
- Exit the application
## Usage
1. Run the TaskTracker.ConsoleApp project
2. Use the menu options (1-4) to:
   - View all tasks
   - Add a new task
   - Mark a task as complete
   - Exit the application
## Dependencies
- .NET 8.0
- Microsoft.Extensions.DependencyInjection (ConsoleApp)
- Moq and xUnit (Tests)
## Running Tests
1. Navigate to TaskTracker.Tests
2. Run `dotnet test`
## Design Decisions
- Uses in-memory storage for simplicity
- Implements Dependency Injection for loose coupling
- Follows Clean Architecture principles
- Includes comprehensive unit tests
- TaskItem properties are immutable except for IsCompleted
