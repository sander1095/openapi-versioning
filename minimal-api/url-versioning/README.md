# URL-Based API Versioning (Minimal APIs)

This implementation uses URL segment versioning where the API version is part of the URL path with ASP.NET Core Minimal APIs.

## Configuration

Uses `UrlSegmentApiVersionReader()` with `SubstituteApiVersionInUrl = true`. See `Program.cs` for full configuration.

## Implementation Approach

Minimal APIs use:
- `NewVersionedApi()` to create a versioned API group
- `MapGroup()` with `{version:apiVersion}` placeholder in routes
- `HasApiVersion()` to assign versions to groups
- `IsApiVersionNeutral()` for version-independent endpoints

Check the source code in `Program.cs` to see the full implementation.

## Running

```bash
dotnet run
```

## Example Requests

Use the included `url-versioning.http` file to test all endpoints.

### URL Examples
- Users v1: `GET http://localhost:5000/api/v1/users`
- Users v2: `GET http://localhost:5000/api/v2/users`
- Scores v1: `GET http://localhost:5000/api/v1/scores`
- Scores v2: `GET http://localhost:5000/api/v2/scores`

### Version-Neutral Endpoint
- Delete User: `DELETE http://localhost:5000/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5000/openapi/v1.json`
- v2: `GET http://localhost:5000/openapi/v2.json`

## Benefits

- Clear and explicit versioning in the URL
- Easy to cache and route
- RESTful approach
- Version is immediately visible in logs and monitoring
- Lightweight and performant Minimal API approach
