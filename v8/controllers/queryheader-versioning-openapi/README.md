# Query Parameter / Header-Based API Versioning (Controllers)

This implementation uses query parameters or HTTP headers for API versioning with ASP.NET Core Controllers. The version is NOT part of the URL path.

## Configuration

The default configuration uses query strings, but can be switched to headers or both. See `Program.cs` for configuration options:
- Query string: `?api-version=1.0`
- HTTP header: `x-api-version: 1.0`
- Or combine both methods

## Implementation Approach

Controllers use:
- `[ApiVersion]` attribute on the controller class to declare supported versions
- `[MapToApiVersion]` attribute on action methods to map them to specific versions
- `[ApiVersionNeutral]` attribute for version-independent endpoints
- Standard controller routing with `[Route]` attributes

Check the controller classes in the `Controllers/` folder to see the full implementation.

## Running

```bash
dotnet run
```

## Example Requests

Use the included `.http` files to test:
- `querystring.http` - Query string versioning examples
- `header.http` - HTTP header versioning examples

### Query String Examples
- Users v1: `GET http://localhost:5017/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5017/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5017/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5017/api/scores?api-version=2.0`

### Version-Neutral Endpoint
- Delete User: `DELETE http://localhost:5017/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5017/openapi/v1.json`
- v2: `GET http://localhost:5017/openapi/v2.json`

## Benefits

- Clean URLs without version numbers
- Easy to change versioning strategy
- Supports multiple versioning methods simultaneously
- Better for APIs with many versions
- Familiar controller-based structure
