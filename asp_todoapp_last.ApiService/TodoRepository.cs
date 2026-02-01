namespace asp_todoapp_last.ApiService;

public class TodoRepository
{
    private List<TodoItem> _todos = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public List<TodoItem> GetAll()
    {
        lock (_lock)
        {
            return _todos.ToList();
        }
    }

    public TodoItem? GetById(int id)
    {
        lock (_lock)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }
    }

    public TodoItem Add(TodoItem todo)
    {
        lock (_lock)
        {
            todo.Id = _nextId++;
            _todos.Add(todo);
            return todo;
        }
    }

    public TodoItem? Update(TodoItem todo)
    {
        lock (_lock)
        {
            var existing = _todos.FirstOrDefault(t => t.Id == todo.Id);
            if (existing == null)
                return null;

            existing.Title = todo.Title;
            existing.IsComplete = todo.IsComplete;
            existing.DueDate = todo.DueDate;
            existing.Priority = todo.Priority;
            return existing;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return false;

            _todos.Remove(todo);
            return true;
        }
    }
}

public class CreateTodoDto
{
    public string? Title { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
}

public class UpdateTodoDto
{
    public string? Title { get; set; }
    public bool? IsComplete { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
}
