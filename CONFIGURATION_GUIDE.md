# Configuration Guide

## Environment Variables

The application respects standard ASP.NET Core environment variables:

### ASPNETCORE_ENVIRONMENT
- **Development**: Enables sample data, Swagger UI, detailed error pages
- **Production**: Minimal logging, no sensitive data in responses

```bash
# Development (default when running from Visual Studio)
set ASPNETCORE_ENVIRONMENT=Development

# Production
set ASPNETCORE_ENVIRONMENT=Production
```

### Port Configuration

#### API Service
```bash
# Default HTTPS port: 7001
# Default HTTP port: 5000

# Override with environment variable:
set ASPNETCORE_URLS=https://localhost:7001;http://localhost:5000
```

#### Web Frontend
```bash
# Default HTTPS port: 7000
# Default HTTP port: 5000

# Override with environment variable:
set ASPNETCORE_URLS=https://localhost:7000;http://localhost:5000
```

### Service Discovery

When running via Aspire AppHost, service discovery is automatic. The connection string is:
```
https+http://apiservice
```

The `https+http://` prefix indicates that HTTPS is preferred, but HTTP is acceptable as a fallback.

---

## Configuration Files

### appSettings.json (API Service)
Located in: `asp_todoapp_last.ApiService/appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### appSettings.json (Web Frontend)
Located in: `asp_todoapp_last.Web/appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### appsettings.Development.json
Development-specific settings override the base appsettings.json when running in Development environment.

---

## Logging Configuration

### Verbosity Levels
- **Trace**: Most detailed (rarely used)
- **Debug**: Detailed diagnostic information
- **Information**: General informational messages
- **Warning**: Warning messages for potential issues
- **Error**: Error messages for failures
- **Critical**: Critical failures that may cause shutdown
- **None**: No logging

### Enabling Debug Logging

#### In code:
```csharp
builder.Logging.SetMinimumLevel(LogLevel.Debug);
```

#### Via appsettings.json:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Debug",
      "Microsoft.AspNetCore.Hosting": "Information"
    }
  }
}
```

---

## CORS Configuration

### Current Configuration
The API has CORS configured to accept requests from any origin:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

### Restricting CORS (Production Recommendation)
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7000", "https://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

app.UseCors("AllowWebFrontend");
```

---

## Service Discovery Configuration

### Local Development
Services are discovered via Aspire's built-in service discovery on localhost.

### Production Deployment
Configure service discovery for your deployment environment:

#### Azure Container Apps
Service discovery works automatically with Azure Container Apps when deployed as a Container App.

#### Kubernetes
Add service discovery configuration:
```csharp
builder.Services.AddServiceDiscovery();
builder.Services.Configure<ServiceDiscoveryOptions>(options =>
{
    options.AllowedSchemes = ["https"];
});
```

---

## Health Check Configuration

### Current Implementation
Both services have health checks configured in `ServiceDefaults/Extensions.cs`:

- `GET /health` - Readiness health check
- `GET /alive` - Liveness health check

### Custom Health Checks
To add application-specific health checks:

```csharp
builder.Services.AddHealthChecks()
    .AddCheck("todo-service", () =>
        todos.Any() ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy()
    );
```

---

## OpenTelemetry Configuration

### Current Setup
OpenTelemetry is configured in `ServiceDefaults/Extensions.cs` for:
- Metrics collection
- Distributed tracing
- Log correlation

### Viewing Telemetry
In the Aspire Dashboard (http://localhost:18888):
- Monitor service health
- View logs from all services
- Track distributed traces
- Monitor resource usage

### Exporting Telemetry
For production, configure exporters:

```csharp
var otelBuilder = services.AddOpenTelemetry();
otelBuilder.UseAzureMonitor();  // Azure Monitor
// or
otelBuilder.UseOtlpExporter();  // Generic OTLP endpoint
```

---

## Resilience Configuration

The application includes automatic resilience patterns via the Service Defaults:

### Policies Included
- **Retry**: Automatic retry for transient failures
- **Timeout**: Request timeout configuration
- **Circuit Breaker**: Fail-fast for failing services
- **Bulkhead Isolation**: Limit concurrent requests

### Customizing Resilience
```csharp
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddStandardResilienceHandler(options =>
    {
        options.Retry.MaxRetryAttempts = 3;
        options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(5);
    });
});
```

---

## Data Storage Configuration

### Current: In-Memory Storage
```csharp
builder.Services.AddSingleton<TodoRepository>();
```

**Advantages**: Simple, fast, no setup required
**Disadvantages**: Data lost on restart, not suitable for production

### Alternative: SQL Database

#### Using Entity Framework Core with SQL Server:
```csharp
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ITodoRepository, EfTodoRepository>();
```

#### Connection String (appsettings.json):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TodoDb;Integrated Security=true;"
  }
}
```

---

## Performance Tuning

### Output Caching (Web Frontend)
Already enabled:
```csharp
builder.Services.AddOutputCache();
app.UseOutputCache();
```

To configure cache duration:
```csharp
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(10);
});
```

### Response Compression
Add response compression for better performance:
```csharp
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

app.UseResponseCompression();
```

### HTTP/2 Configuration
```csharp
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.Http2.MaxStreamsPerConnection = 100;
});
```

---

## SSL/TLS Configuration

### Development
Self-signed certificates are automatically generated and trusted.

### Production
Configure with your own certificates:

```csharp
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps("/path/to/certificate.pfx", "password");
    });
});
```

### Azure Key Vault Integration
```csharp
var keyVaultUrl = new Uri("https://yourkeyvault.vault.azure.net/");
builder.Configuration.AddAzureKeyVault(keyVaultUrl, new DefaultAzureCredential());
```

---

## Rate Limiting Configuration

Add rate limiting for production:

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("api", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 100;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 5;
    });
});

app.UseRateLimiter();
```

---

## Database Initialization

For production with persistent storage, run migrations:

```bash
# Create initial migration
dotnet ef migrations add InitialCreate --project asp_todoapp_last.ApiService

# Apply migrations
dotnet ef database update --project asp_todoapp_last.ApiService
```

---

## Monitoring & Diagnostics

### Aspire Dashboard Features
- Real-time service monitoring
- Log viewer with filtering
- Trace visualization
- Resource utilization charts
- Health status indicators

### Manual Debugging
```bash
# Enable verbose logging
set ASPNETCORE_ENVIRONMENT=Development

# Run with detailed output
dotnet run --project asp_todoapp_last.ApiService --verbose
```

---

## Environment-Specific Configuration

### Development Environment
- Sample data loaded automatically
- Swagger UI enabled
- Detailed error pages
- Console logging

### Production Environment
- No sample data
- No Swagger UI
- Generic error responses
- Minimal logging (File/Application Insights)

---

## Quick Configuration Changes

### Change API Port
In `AppHost.cs`:
```csharp
builder.AddProject<Projects.asp_todoapp_last_ApiService>("apiservice")
    .WithHttpEndpoint(port: 8001);  // Custom port
```

### Change Web Port
In `AppHost.cs`:
```csharp
builder.AddProject<Projects.asp_todoapp_last_Web>("webfrontend")
    .WithHttpEndpoint(port: 8000);  // Custom port
```

### Disable Sample Data
In `Program.cs` (API Service):
```csharp
// Comment out or condition the initialization
if (app.Environment.IsDevelopment() && false)  // Disable
{
    InitializeSampleData(todoRepository);
}
```

---

## Troubleshooting Configuration Issues

### Port Already in Use
```bash
# Find process using port
netstat -ano | findstr :7000

# Kill process
taskkill /PID <PID> /F
```

### Service Discovery Not Working
- Verify Aspire AppHost is running
- Check that service names match in AppHost.cs
- Enable debug logging to see discovery messages

### CORS Errors
- Verify CORS policy is configured
- Check allowed origins
- Ensure API is running on expected address

### Certificate Issues
- Clear certificate cache: `dotnet dev-certs https --clean`
- Trust certificates: `dotnet dev-certs https --trust`

---

## Best Practices

1. **Development vs. Production**: Use environment-specific configurations
2. **Secrets Management**: Store sensitive data in Azure Key Vault or similar
3. **Health Checks**: Regularly monitor service health
4. **Logging**: Maintain appropriate log levels
5. **CORS**: Restrict to specific origins in production
6. **Resilience**: Configure appropriate retry and timeout policies
7. **Security**: Use HTTPS, validate inputs, authenticate users
8. **Performance**: Enable caching, compression, and efficient database queries

---

## Additional Resources

- [ASP.NET Core Configuration Documentation](https://learn.microsoft.com/aspnet/core/fundamentals/configuration)
- [Aspire Configuration Guide](https://learn.microsoft.com/dotnet/aspire/fundamentals/)
- [Kestrel Web Server Documentation](https://learn.microsoft.com/aspnet/core/fundamentals/servers/kestrel)
