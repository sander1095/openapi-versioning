# Query Parameter / Header-Based API Versioning (Minimal APIs)

This implementation uses query parameters or HTTP headers for API versioning with ASP.NET Core Minimal APIs. The version is NOT part of the URL path.

## Configuration

The default configuration uses query strings, but can be switched to headers or both. See `Program.cs` for configuration options:
- Query string: `?api-version=1.0`
- HTTP header: `x-api-version: 1.0`
- Or combine both methods

## Implementation Approach

Minimal APIs use:
- `NewVersionedApi()` to create a versioned API group
- `MapGroup()` to define route groups
- `HasApiVersion()` to assign versions to groups
- `IsApiVersionNeutral()` for version-independent endpoints

Check the source code in `Program.cs` to see the full implementation.

## Running

```bash
dotnet run
```

## Example Requests

Use the included `.http` files to test:
- `querystring.http` - Query string versioning examples
- `header.http` - HTTP header versioning examples

### Query String Examples
- Users v1: `GET http://localhost:5022/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5022/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5022/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5022/api/scores?api-version=2.0`

### Version-Neutral Endpoint
- Delete User: `DELETE http://localhost:5022/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5022/openapi/v1.json`
- v2: `GET http://localhost:5022/openapi/v2.json`

## Benefits

- Clean URLs without version numbers
- Easy to change versioning strategy
- Supports multiple versioning methods simultaneously
- Better for APIs with many versions
- Lightweight and performant Minimal API approach
