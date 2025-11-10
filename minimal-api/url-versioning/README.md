# URL-Based API Versioning

This implementation uses URL segment versioning where the API version is part of the URL path.

## Configuration

```csharp
x.ApiVersionReader = new UrlSegmentApiVersionReader();
options.SubstituteApiVersionInUrl = true;
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
