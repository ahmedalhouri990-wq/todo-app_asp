namespace asp_todoapp_last.Web;

public class TodoApiClient
{
    private readonly HttpClient _httpClient;

    public TodoApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TodoItemDto>?> GetTodosAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<TodoItemDto>>("/todos");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching todos: {ex.Message}");
            return null;
        }
    }

    public async Task<TodoItemDto?> GetTodoAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<TodoItemDto>($"/todos/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching todo: {ex.Message}");
            return null;
        }
    }

    public async Task<TodoItemDto?> CreateTodoAsync(CreateTodoDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/todos", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TodoItemDto>();
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating todo: {ex.Message}");
            return null;
        }
    }

    public async Task<TodoItemDto?> UpdateTodoAsync(int id, UpdateTodoDto dto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/todos/{id}", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TodoItemDto>();
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating todo: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/todos/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting todo: {ex.Message}");
            return false;
        }
    }
}

public class TodoItemDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? DueDate { get; set; }
    public PriorityDto Priority { get; set; }
}

public class CreateTodoDto
{
    public string? Title { get; set; }
    public DateTime? DueDate { get; set; }
    public PriorityDto Priority { get; set; } = PriorityDto.Medium;
}

public class UpdateTodoDto
{
    public string? Title { get; set; }
    public bool? IsComplete { get; set; }
    public DateTime? DueDate { get; set; }
    public PriorityDto Priority { get; set; } = PriorityDto.Medium;
}

public enum PriorityDto
{
    Low,
    Medium,
    High
}
