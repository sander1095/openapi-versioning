# Query Parameter / Header-Based API Versioning (Minimal APIs)

This implementation uses query parameters or HTTP headers for API versioning with ASP.NET Core Minimal APIs. The version is NOT part of the URL path.

## Configuration

Default is query string:
```csharp
options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
```

Alternative header-based:
```csharp
options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
```

Or combine both:
```csharp
options.ApiVersionReader = ApiVersionReader.Combine(
    new QueryStringApiVersionReader("api-version"),
    new HeaderApiVersionReader("x-api-version")
);
```

## Minimal API Implementation

Minimal APIs use `MapGroup()` and `HasApiVersion()` to define versioned endpoints:

```csharp
var usersApi = app.NewVersionedApi("Users");

var usersv1 = usersApi.MapGroup("api/users")
    .HasApiVersion(1.0);

var usersv2 = usersApi.MapGroup("api/users")
    .HasApiVersion(2.0);

usersv1.MapGet("", () =>
{
    return TypedResults.Ok(new[]
    {
        new Userv1(1, "John Doe", "johndoe@example.com"),
        new Userv1(2, "Alice Dewett", "alice@example.com"),
    });
});
```

For version-neutral endpoints:
```csharp
var usersNeutral = usersApi.MapGroup("api/users")
    .IsApiVersionNeutral();

usersNeutral.MapDelete("{id:int}", (int id) =>
{
    return TypedResults.NoContent();
});
```

## Running

```bash
dotnet run
```

## Example URLs (Query String)

- **Users v1**: `GET http://localhost:5001/api/users?api-version=1.0`
- **Users v2**: `GET http://localhost:5001/api/users?api-version=2.0`
- **Scores v1**: `GET http://localhost:5001/api/scores?api-version=1.0`
- **Scores v2**: `GET http://localhost:5001/api/scores?api-version=2.0`
- **Delete User**: `DELETE http://localhost:5001/api/users/{id}` (version-neutral endpoint)

## Example with Header

```bash
curl -H "x-api-version: 1.0" http://localhost:5001/api/users
```

## OpenAPI

- **v1 OpenAPI**: `GET http://localhost:5001/openapi/v1.json`
- **v2 OpenAPI**: `GET http://localhost:5001/openapi/v2.json`

## Benefits

- Clean URLs without version numbers
- Easy to change versioning strategy
- Supports multiple versioning methods simultaneously
- Better for APIs with many versions
- Lightweight and performant Minimal API approach
