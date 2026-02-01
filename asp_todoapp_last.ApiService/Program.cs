using asp_todoapp_last.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add in-memory database for todos
var todoRepository = new TodoRepository();
builder.Services.AddSingleton(todoRepository);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.MapGet("/", () => "Todo API service is running. Use /todos endpoints to manage your todos.");

// Todo endpoints
var todoGroup = app.MapGroup("/todos")
    .WithName("TodoItems")
    .WithOpenApi();

todoGroup.MapGet("/", GetAllTodos)
    .WithName("GetAllTodos")
    .WithSummary("Get all todo items");

todoGroup.MapGet("/{id}", GetTodoById)
    .WithName("GetTodoById")
    .WithSummary("Get a specific todo item");

todoGroup.MapPost("/", CreateTodo)
    .WithName("CreateTodo")
    .WithSummary("Create a new todo item");

todoGroup.MapPut("/{id}", UpdateTodo)
    .WithName("UpdateTodo")
    .WithSummary("Update a todo item");

todoGroup.MapDelete("/{id}", DeleteTodo)
    .WithName("DeleteTodo")
    .WithSummary("Delete a todo item");

static IResult GetAllTodos(TodoRepository repo)
{
    return Results.Ok(repo.GetAll());
}

static IResult GetTodoById(int id, TodoRepository repo)
{
    var todo = repo.GetById(id);
    if (todo == null)
        return Results.NotFound();
    return Results.Ok(todo);
}

static IResult CreateTodo(CreateTodoDto dto, TodoRepository repo)
{
    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest("Title is required");

    var todo = new TodoItem
    {
        Title = dto.Title,
        IsComplete = false,
        DueDate = dto.DueDate,
        Priority = dto.Priority
    };

    var created = repo.Add(todo);
    return Results.Created($"/todos/{created.Id}", created);
}

static IResult UpdateTodo(int id, UpdateTodoDto dto, TodoRepository repo)
{
    var todo = repo.GetById(id);
    if (todo == null)
        return Results.NotFound();

    if (!string.IsNullOrWhiteSpace(dto.Title))
        todo.Title = dto.Title;

    if (dto.IsComplete.HasValue)
        todo.IsComplete = dto.IsComplete.Value;

    if (dto.DueDate.HasValue)
        todo.DueDate = dto.DueDate.Value;

    if (Enum.IsDefined(typeof(Priority), dto.Priority))
        todo.Priority = dto.Priority;

    var updated = repo.Update(todo);
    return Results.Ok(updated);
}

static IResult DeleteTodo(int id, TodoRepository repo)
{
    if (!repo.Delete(id))
        return Results.NotFound();

    return Results.NoContent();
}

app.MapDefaultEndpoints();

app.Run();
