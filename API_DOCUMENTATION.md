# API Documentation

## Overview
The To-Do API provides RESTful endpoints for managing todo items. All endpoints return JSON responses and use standard HTTP status codes.

## Base URL
```
https+http://apiservice/todos
```

When running via Aspire AppHost, the API is automatically discovered and available to the Blazor frontend.

## Data Models

### TodoItem (Response)
```json
{
  "id": 1,
  "title": "Sample Task",
  "isComplete": false,
  "dueDate": "2024-12-31T00:00:00",
  "priority": 1
}
```

**Fields:**
- `id` (integer): Unique identifier for the todo item
- `title` (string): Task description
- `isComplete` (boolean): Completion status
- `dueDate` (datetime, nullable): Due date for the task
- `priority` (integer): Priority level (0=Low, 1=Medium, 2=High)

### Priority Enum
```
0 = Low
1 = Medium
2 = High
```

## Endpoints

### 1. Get All Todos
**Endpoint:** `GET /todos`

**Description:** Retrieves all todo items.

**Response:**
```http
HTTP/1.1 200 OK
Content-Type: application/json

[
  {
    "id": 1,
    "title": "Learn Aspire",
    "isComplete": false,
    "dueDate": "2024-12-31T00:00:00",
    "priority": 2
  },
  {
    "id": 2,
    "title": "Build Todo App",
    "isComplete": false,
    "dueDate": null,
    "priority": 1
  }
]
```

**cURL Example:**
```bash
curl -X GET https://localhost:7001/todos
```

---

### 2. Get Single Todo
**Endpoint:** `GET /todos/{id}`

**Description:** Retrieves a specific todo item by ID.

**Parameters:**
- `id` (path, required): Todo item ID

**Responses:**

**Success (200):**
```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 1,
  "title": "Learn Aspire",
  "isComplete": false,
  "dueDate": "2024-12-31T00:00:00",
  "priority": 2
}
```

**Not Found (404):**
```http
HTTP/1.1 404 Not Found
```

**cURL Example:**
```bash
curl -X GET https://localhost:7001/todos/1
```

---

### 3. Create Todo
**Endpoint:** `POST /todos`

**Description:** Creates a new todo item.

**Request Body:**
```json
{
  "title": "New Task",
  "priority": 1,
  "dueDate": "2024-12-31T00:00:00"
}
```

**Request Fields:**
- `title` (string, required): Task description
- `priority` (integer, optional): Priority level (default: 1=Medium)
- `dueDate` (datetime, optional): Due date for the task

**Response (201 Created):**
```http
HTTP/1.1 201 Created
Content-Type: application/json
Location: /todos/1

{
  "id": 1,
  "title": "New Task",
  "isComplete": false,
  "dueDate": "2024-12-31T00:00:00",
  "priority": 1
}
```

**Error Responses:**

**Bad Request (400) - Missing Title:**
```http
HTTP/1.1 400 Bad Request
Content-Type: application/json

"Title is required"
```

**cURL Example:**
```bash
curl -X POST https://localhost:7001/todos \
  -H "Content-Type: application/json" \
  -d '{
    "title": "New Task",
    "priority": 2,
    "dueDate": "2024-12-31T00:00:00"
  }'
```

---

### 4. Update Todo
**Endpoint:** `PUT /todos/{id}`

**Description:** Updates an existing todo item. All fields are optional - only provided fields are updated.

**Parameters:**
- `id` (path, required): Todo item ID

**Request Body:**
```json
{
  "title": "Updated Task",
  "isComplete": true,
  "priority": 1,
  "dueDate": "2024-12-31T00:00:00"
}
```

**Request Fields (all optional):**
- `title` (string): Updated task description
- `isComplete` (boolean): Updated completion status
- `priority` (integer): Updated priority level
- `dueDate` (datetime, nullable): Updated due date

**Response (200 OK):**
```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 1,
  "title": "Updated Task",
  "isComplete": true,
  "dueDate": "2024-12-31T00:00:00",
  "priority": 1
}
```

**Not Found (404):**
```http
HTTP/1.1 404 Not Found
```

**cURL Example - Mark as Complete:**
```bash
curl -X PUT https://localhost:7001/todos/1 \
  -H "Content-Type: application/json" \
  -d '{
    "isComplete": true
  }'
```

**cURL Example - Update All Fields:**
```bash
curl -X PUT https://localhost:7001/todos/1 \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Updated Task",
    "isComplete": true,
    "priority": 2,
    "dueDate": "2024-12-31T00:00:00"
  }'
```

---

### 5. Delete Todo
**Endpoint:** `DELETE /todos/{id}`

**Description:** Deletes a todo item.

**Parameters:**
- `id` (path, required): Todo item ID

**Response Codes:**

**Success (204 No Content):**
```http
HTTP/1.1 204 No Content
```

**Not Found (404):**
```http
HTTP/1.1 404 Not Found
```

**cURL Example:**
```bash
curl -X DELETE https://localhost:7001/todos/1
```

---

## Status Codes

| Code | Description |
|------|-------------|
| 200  | OK - Request succeeded |
| 201  | Created - Resource successfully created |
| 204  | No Content - Request succeeded, no content to return |
| 400  | Bad Request - Invalid input data |
| 404  | Not Found - Resource not found |
| 500  | Internal Server Error - Server encountered an error |

---

## Error Handling

The API returns standard HTTP status codes and error messages.

### Example Error Response:
```http
HTTP/1.1 400 Bad Request
Content-Type: application/json

"Title is required"
```

---

## Common Use Cases

### 1. Create a High Priority Task Due Tomorrow
```bash
curl -X POST https://localhost:7001/todos \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Urgent: Complete project proposal",
    "priority": 2,
    "dueDate": "2024-01-16T00:00:00"
  }'
```

### 2. Complete a Task
```bash
curl -X PUT https://localhost:7001/todos/1 \
  -H "Content-Type: application/json" \
  -d '{
    "isComplete": true
  }'
```

### 3. Get All Todos and Filter Locally
```bash
# Get all todos and save to file
curl -X GET https://localhost:7001/todos > todos.json

# View the JSON
cat todos.json
```

### 4. Update Only the Priority
```bash
curl -X PUT https://localhost:7001/todos/5 \
  -H "Content-Type: application/json" \
  -d '{
    "priority": 0
  }'
```

---

## OpenAPI/Swagger

The API provides OpenAPI 3.0 documentation available at:
```
https://localhost:7001/openapi/v1.json
```

View the Swagger UI (in development):
```
https://localhost:7001/swagger
```

---

## Rate Limiting & Throttling

Currently, there are no rate limits configured. In production deployment, consider adding:
- Rate limiting per client/IP
- Throttling for intensive operations
- Caching strategies for frequently accessed data

---

## Authentication & Authorization

The current implementation has no authentication. In production, consider:
- Adding JWT authentication
- Role-based access control (RBAC)
- API key validation
- OAuth 2.0 integration

---

## Versioning

The API currently follows RESTful conventions without explicit versioning. Future versions may include:
- API version in URL path: `/v1/todos`
- API version in header: `Accept: application/json; version=1`

---

## Performance Considerations

### In-Memory Storage
Currently, todo items are stored in memory, which means:
- Data is lost when the application restarts
- All data is loaded into memory
- Suitable for demos and development only

### Production Implementation
For production, consider:
- Database persistence (SQL Server, PostgreSQL, etc.)
- Caching strategies (Redis, In-Memory Cache)
- Pagination for large datasets
- Indexing on frequently queried fields

---

## Support

For issues or questions about the API:
1. Check the console output for detailed error messages
2. Enable debug logging in `appsettings.json`
3. Review the application logs in the Aspire Dashboard
4. Refer to the main README.md for architectural overview
