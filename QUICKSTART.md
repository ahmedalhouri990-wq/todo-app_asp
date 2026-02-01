# Quick Start Guide - ASP.NET Aspire To-Do App

## 🚀 Running the Application

### Step 1: Start the Application
```bash
dotnet run --project asp_todoapp_last.AppHost
```

The application will start and you'll see output indicating the services are running.

### Step 2: Access the Application
- **Web Frontend**: Open your browser and navigate to the URL shown (typically `https://localhost:7000`)
- **Aspire Dashboard**: Access `http://localhost:18888` to monitor your services

### Step 3: Start Using the App
1. You'll be greeted with the Home page explaining the app features
2. Click the **"Go to My Todos →"** button or use the **"Todos"** link in the navigation menu
3. You'll see sample todos pre-loaded in development mode

## 📝 Basic Operations

### Adding a Todo
1. Click the **"+ Add New Todo"** button
2. Enter a task title (e.g., "Complete project documentation")
3. Select a priority level (Low, Medium, High)
4. Optionally set a due date
5. Click **"Save"**

### Editing a Todo
1. Find the todo you want to edit
2. Click the **"Edit"** button
3. Modify the title, priority, or due date
4. Click **"Save Changes"**

### Completing a Todo
1. Check the checkbox next to a todo item
2. The task will be marked as complete (appears with strikethrough)

### Deleting a Todo
1. Click the **"Delete"** button on the todo
2. The task is immediately removed

## 🔍 Filtering & Sorting

### Filter by Priority
- Select "Low", "Medium", or "High" from the Priority filter dropdown
- Shows only todos with the selected priority level

### Filter by Status
- **All Tasks**: Shows everything
- **Pending**: Shows only incomplete tasks
- **Completed**: Shows only completed tasks

### Sort Options
- **Date (Oldest First)**: Shows todos with earliest due dates first
- **Date (Newest First)**: Shows todos with latest due dates first
- **Priority (High to Low)**: Shows high priority tasks first
- **Priority (Low to High)**: Shows low priority tasks first

### Refresh
Click the **"🔄 Refresh"** button to reload the todo list from the server.

## 📊 Dashboard Statistics

At the bottom of the todos page, you'll see:
- **Total**: Total number of todos
- **Completed**: Number of completed todos
- **Pending**: Number of incomplete todos

## 🔗 API Endpoints (for advanced users)

The API is available at `https+http://apiservice` when running via Aspire.

### Test the API with curl
```bash
# Get all todos
curl https://localhost:7001/todos

# Create a new todo
curl -X POST https://localhost:7001/todos \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Test Todo",
    "priority": 1,
    "dueDate": "2024-12-31T00:00:00"
  }'

# Get OpenAPI documentation
curl https://localhost:7001/openapi/v1.json
```

## 💡 Tips & Tricks

1. **Priority Color Coding**:
   - 🔴 Red = High priority
   - 🟡 Yellow = Medium priority
   - 🔵 Blue = Low priority

2. **Combine Filters**: You can use priority and status filters together for precise results

3. **Sample Data**: In development mode, the app starts with 5 sample todos to explore

4. **Service Discovery**: The Blazor frontend automatically finds the API service - no manual configuration needed!

## 🛠️ Troubleshooting

### Application won't start
- Ensure .NET 10 SDK is installed: `dotnet --version`
- Try clearing the bin and obj folders: `dotnet clean`
- Rebuild the solution: `dotnet build`

### Can't access the app
- Check that the correct URL is shown in the terminal output
- Ensure port 7000 (or shown port) isn't already in use
- Check your firewall settings

### API communication errors
- Verify the Aspire AppHost is running (should show "apiservice" and "webfrontend")
- Check that both services have green health checks in the dashboard

### No sample data showing
- Sample data is only loaded in Development mode
- Verify `ASPNETCORE_ENVIRONMENT=Development` is set

## 📚 Learn More

- [ASP.NET Aspire Documentation](https://learn.microsoft.com/dotnet/aspire/)
- [Blazor Interactive Server Components](https://learn.microsoft.com/aspnet/core/blazor/components/rendering-modes)
- [Minimal APIs in ASP.NET Core](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)

## 🎉 You're Ready!

Start organizing your tasks with this modern, full-stack ASP.NET Aspire application. Happy task managing!
