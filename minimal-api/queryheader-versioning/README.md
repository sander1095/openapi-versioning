# Query Parameter / Header-Based API Versioning

This implementation uses query parameters or HTTP headers for API versioning. The version is NOT part of the URL path.

## Configuration

Default is query string:
```csharp
x.ApiVersionReader = new QueryStringApiVersionReader("api-version");
```

Alternative header-based:
```csharp
x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
```

Or combine both:
```csharp
x.ApiVersionReader = ApiVersionReader.Combine(
    new QueryStringApiVersionReader("api-version"),
    new HeaderApiVersionReader("x-api-version")
);
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
