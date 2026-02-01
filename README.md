# ASP.NET Aspire To-Do App

[![.NET Build](https://github.com/YOUR_USERNAME/asp-todoapp/workflows/.NET%20Build%20and%20Test/badge.svg)](https://github.com/YOUR_USERNAME/asp-todoapp/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A complete full-stack to-do application built with **ASP.NET Aspire**, featuring a REST API backend and an interactive Blazor Server frontend.

## 🎯 Features

### Core Functionality
- ✅ **Full CRUD Operations**: Create, Read, Update, and Delete todo items
- 📝 **Task Management**: Add, edit, and manage your daily tasks
- ✔️ **Mark Complete**: Toggle task completion status
- 🎯 **Priority Levels**: Set priorities (Low, Medium, High) for each task
- 📅 **Due Dates**: Assign due dates to tasks
- 🔍 **Advanced Filtering**: Filter by priority and completion status
- 📊 **Smart Sorting**: Sort by due date or priority (ascending/descending)
- 💾 **Persistent Storage**: Tasks stored in-memory during application lifetime

## 🏗️ Architecture

### Projects

1. **asp_todoapp_last.ApiService** - REST API Backend
   - ASP.NET Core Minimal APIs
   - CRUD endpoints for todo management
   - CORS enabled for Blazor communication
   - OpenAPI/Swagger documentation

2. **asp_todoapp_last.Web** - Blazor Server Frontend
   - Interactive Blazor Server components
   - Responsive Bootstrap UI
   - Real-time filtering and sorting
   - HttpClient integration with API

3. **asp_todoapp_last.AppHost** - Aspire Application Host
   - Orchestrates API and Web services
   - Service discovery configuration
   - Health checks setup

4. **asp_todoapp_last.ServiceDefaults** - Shared Configuration
   - OpenTelemetry setup
   - Health checks configuration
   - Service discovery configuration
   - Resilience patterns

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK or later
- Visual Studio 2022 or VS Code with C# extension

### Running the Application

#### Option 1: Using Aspire Dashboard (Recommended)
```bash
dotnet run --project asp_todoapp_last.AppHost
```
This will start the Aspire host and you can access:
- **Web Frontend**: https://localhost:7000 (or the port shown in terminal)
- **Aspire Dashboard**: Usually http://localhost:18888

#### Option 2: Running Individual Services
```bash
# Terminal 1 - API Service
dotnet run --project asp_todoapp_last.ApiService

# Terminal 2 - Web Frontend
dotnet run --project asp_todoapp_last.Web
```

## 📋 API Endpoints

All endpoints are prefixed with `/todos`

### Get All Todos
```
GET /todos
```

### Get Todo by ID
```
GET /todos/{id}
```

### Create Todo
```
POST /todos
Content-Type: application/json

{
  "title": "Sample task",
  "priority": 1,
  "dueDate": "2024-12-31T00:00:00"
}
```

### Update Todo
```
PUT /todos/{id}
Content-Type: application/json

{
  "title": "Updated task",
  "isComplete": true,
  "priority": 2,
  "dueDate": "2024-12-31T00:00:00"
}
```

### Delete Todo
```
DELETE /todos/{id}
```

## 🎨 UI Features

### Main Dashboard
- **Todo List View**: Display all todos with status, priority, and due date
- **Quick Actions**: Checkbox to mark complete, Edit, and Delete buttons
- **Task Statistics**: Shows total, completed, and pending task counts

### Add/Edit Form
- Title input field
- Priority dropdown (Low, Medium, High)
- Due date picker
- Save and Cancel buttons

### Filtering Panel
- **Priority Filter**: Filter by Low, Medium, or High priority
- **Status Filter**: Show All, Pending, or Completed tasks
- **Sort Options**: Sort by date (ascending/descending) or priority

## 💾 Data Models

### Priority Enum
```csharp
public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2
}
```

### TodoItem (API)
```csharp
public class TodoItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; }
}
```

### TodoItemDto (Blazor)
Mirrors the TodoItem structure for client-side representation.

## 🔄 Component Communication

The Blazor frontend communicates with the API service through `TodoApiClient`, which:
- Manages HTTP requests to the API
- Handles error scenarios gracefully
- Provides async methods for all CRUD operations
- Automatically manages JSON serialization/deserialization

## 📡 Service Discovery

Thanks to Aspire's built-in service discovery:
- The Web frontend automatically discovers the API service at `https+http://apiservice`
- Services communicate internally using service names
- No hardcoded IP addresses or ports needed

## 🧪 Testing the Application

1. **Add a Todo**:
   - Click "+ Add New Todo"
   - Enter a title (e.g., "Learn Aspire")
   - Select priority and due date
   - Click "Save"

2. **Mark as Complete**:
   - Check the checkbox next to a todo
   - Task will appear with strikethrough

3. **Edit a Todo**:
   - Click "Edit" button
   - Modify the title, priority, or due date
   - Click "Save Changes"

4. **Delete a Todo**:
   - Click "Delete" button
   - Todo is immediately removed

5. **Filter and Sort**:
   - Use the filter dropdowns to narrow results
   - Change sort order using the "Sort By" dropdown
   - Click "Refresh" to reload data

## 🛠️ Development

### Project Structure
```
asp_todoapp_last/
├── asp_todoapp_last.ApiService/
│   ├── Program.cs
│   ├── TodoItem.cs
│   └── TodoRepository.cs
├── asp_todoapp_last.Web/
│   ├── Program.cs
│   ├── TodoApiClient.cs
│   └── Components/
│       ├── Pages/
│       │   ├── Home.razor
│       │   ├── Todos.razor
│       │   └── ...
│       └── Layout/
│           └── NavMenu.razor
├── asp_todoapp_last.AppHost/
│   └── AppHost.cs
└── asp_todoapp_last.ServiceDefaults/
    └── Extensions.cs
```

### Building from Source
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

## 🌐 Deployment

This application is designed to work with Azure Container Apps or other container-based deployments through Aspire's deployment tools.

### Publishing
```bash
dotnet publish -c Release
```

## 📚 References

- [ASP.NET Aspire Documentation](https://learn.microsoft.com/dotnet/aspire/fundamentals/)
- [Blazor Server Documentation](https://learn.microsoft.com/aspnet/core/blazor/?view=aspnetcore-10.0)
- [ASP.NET Core Minimal APIs](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)
- [Service Discovery in .NET Aspire](https://learn.microsoft.com/dotnet/aspire/service-discovery/)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

## 📄 License

This project is part of the ASP.NET Aspire samples and is provided as-is for educational purposes.

## 🎉 Enjoy Your To-Do App!

Start managing your tasks efficiently with this modern, full-stack ASP.NET Aspire application!
