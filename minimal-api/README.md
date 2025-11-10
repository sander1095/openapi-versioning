# ASP.NET Core API Versioning with Minimal APIs

This folder contains Minimal API implementations of API versioning strategies using the `Asp.Versioning.Http` library.

## Projects

### 1. queryheader-versioning
Demonstrates query parameter and header-based API versioning using Minimal APIs.
- Port: `5001`
- Versioning: Query string (`?api-version=1.0`) or HTTP header (`x-api-version: 1.0`)
- [README](./queryheader-versioning/README.md)

### 2. url-versioning
Demonstrates URL segment-based API versioning using Minimal APIs.
- Port: `5000`
- Versioning: URL path (`/api/v1/users`)
- [README](./url-versioning/README.md)

## Key Features of Minimal API Implementation

Minimal APIs use:
- `MapGroup()` to define route groups
- `HasApiVersion()` to assign versions to groups
- `IsApiVersionNeutral()` for version-independent endpoints
- Lambda expressions or local functions for endpoint handlers
- Record types for DTOs

Example:
```csharp
var usersApi = app.NewVersionedApi("Users");

var usersv1 = usersApi.MapGroup("api/users")
    .HasApiVersion(1.0);

usersv1.MapGet("", () => 
{
    return TypedResults.Ok(new[] { new Userv1(1, "John", "john@example.com") });
});
```

## Common Features

Both implementations include:
- Two API endpoints: Users and Scores
- Two versions (v1 and v2) with different response models
- Version-neutral DELETE endpoint for users
- OpenAPI/Swagger documentation for each version
- HTTP test files for easy testing

## Running the Projects

```bash
# Query/Header versioning (port 5001)
cd queryheader-versioning
dotnet run

# URL versioning (port 5000)
cd url-versioning
dotnet run
```

## Testing

Each project includes `.http` files that can be used with VS Code's REST Client extension or Visual Studio's HTTP client.

## Learn More

- [ASP.NET API Versioning Wiki](https://github.com/dotnet/aspnet-api-versioning/wiki)
- [Minimal APIs Overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
