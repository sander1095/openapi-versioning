# Query Parameter / Header-Based API Versioning (Minimal APIs with AOT)

This implementation uses query parameters or HTTP headers for API versioning with ASP.NET Core Minimal APIs, **optimized for Native AOT compilation**. The version is NOT part of the URL path.

## AOT (Ahead-of-Time) Compilation

This project is configured for Native AOT compilation, which provides:
- **Faster startup time** - The app starts almost instantly
- **Smaller memory footprint** - Reduced memory usage
- **Smaller deployment size** - Trimmed, self-contained executables
- **Better performance** - Pre-compiled native code

### AOT-Specific Configuration

The project uses:
- `WebApplication.CreateSlimBuilder()` instead of `CreateBuilder()` for minimal dependencies
- `JsonSerializerContext` for source-generated JSON serialization
- `PublishAot=true` in the project file
- `InvariantGlobalization=true` for reduced size

All response types are registered in the `AppJsonSerializerContext` to ensure AOT compatibility.

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
- Source-generated JSON serialization for AOT compatibility

Check the source code in `Program.cs` to see the full implementation.

## Running

```bash
# Development
dotnet run

# Build and publish with AOT
dotnet publish -c Release
```

## Example Requests

Use the included `.http` files to test:
- `querystring.http` - Query string versioning examples
- `header.http` - HTTP header versioning examples

### Query String Examples
- Users v1: `GET http://localhost:5020/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5020/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5020/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5020/api/scores?api-version=2.0`

### Version-Neutral Endpoint
- Delete User: `DELETE http://localhost:5020/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5020/openapi/v1.json`
- v2: `GET http://localhost:5020/openapi/v2.json`

## Benefits

- Clean URLs without version numbers
- Easy to change versioning strategy
- Supports multiple versioning methods simultaneously
- Better for APIs with many versions
- Lightweight and performant Minimal API approach
- **Native AOT compilation** for optimal performance and deployment size

## Learn More

- [Native AOT deployment](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/native-aot)
- [JSON source generation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)
