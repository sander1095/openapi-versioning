# URL-Based API Versioning (Minimal APIs)

This implementation uses URL segment versioning where the API version is part of the URL path with ASP.NET Core Minimal APIs.

## Configuration

```csharp
options.ApiVersionReader = new UrlSegmentApiVersionReader();
options.SubstituteApiVersionInUrl = true;
```

## Minimal API Implementation

Minimal APIs use `MapGroup()` with the version placeholder `{version:apiVersion}` and `HasApiVersion()`:

```csharp
var usersApi = app.NewVersionedApi("Users");

var usersv1 = usersApi.MapGroup("api/v{version:apiVersion}/users")
    .HasApiVersion(1.0);

var usersv2 = usersApi.MapGroup("api/v{version:apiVersion}/users")
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

For version-neutral endpoints, use a route without the version placeholder:
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

## Example URLs

- **Users v1**: `GET http://localhost:5000/api/v1/users`
- **Users v2**: `GET http://localhost:5000/api/v2/users`
- **Scores v1**: `GET http://localhost:5000/api/v1/scores`
- **Scores v2**: `GET http://localhost:5000/api/v2/scores`
- **Delete User**: `DELETE http://localhost:5000/api/users/{id}` (version-neutral endpoint)

## OpenAPI

- **v1 OpenAPI**: `GET http://localhost:5000/openapi/v1.json`
- **v2 OpenAPI**: `GET http://localhost:5000/openapi/v2.json`

## Benefits

- Clear and explicit versioning in the URL
- Easy to cache and route
- RESTful approach
- Version is immediately visible in logs and monitoring
- Lightweight and performant Minimal API approach
