# ASP.NET Core API Versioning with Controllers

This folder contains controller-based implementations of API versioning strategies using the `Asp.Versioning` library.

## Projects

### 1. queryheader-versioning
Demonstrates query parameter and header-based API versioning using controllers.
- Port: `5001`
- Versioning: Query string (`?api-version=1.0`) or HTTP header (`x-api-version: 1.0`)
- [README](./queryheader-versioning/README.md)

### 2. url-versioning
Demonstrates URL segment-based API versioning using controllers.
- Port: `5000`
- Versioning: URL path (`/api/v1/users`)
- [README](./url-versioning/README.md)

## Key Differences from Minimal APIs

Controllers use:
- `[ApiController]` attribute for automatic model validation
- `[Route]` attributes for routing configuration
- `[MapToApiVersion]` to specify which version(s) each action supports
- `[ApiVersionNeutral]` for version-independent endpoints
- Traditional action methods returning `IActionResult`

Minimal APIs use:
- `MapGroup()` and route templates
- `HasApiVersion()` for version assignment
- `IsApiVersionNeutral()` for version-independent endpoints
- Inline lambda functions or local methods

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
- [API Versioning in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/versioning)
