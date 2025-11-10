````markdown
# URL-Based API Versioning (Controllers)

This implementation uses URL segment versioning where the API version is part of the URL path with ASP.NET Core Controllers.

## Configuration

```csharp
x.ApiVersionReader = new UrlSegmentApiVersionReader();
options.SubstituteApiVersionInUrl = true;
```

## Controller Implementation

Controllers use the `[Route]` attribute with a version placeholder, `[ApiVersion]` on the controller, and `[MapToApiVersion]` on action methods:

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion(1.0)]
    public IActionResult GetV1() { ... }

    [HttpGet]
    [MapToApiVersion(2.0)]
    public IActionResult GetV2() { ... }
}
```

For version-neutral endpoints, use a separate controller without the version placeholder:

```csharp
[ApiController]
[ApiVersionNeutral]
[Route("api/users")]
public class UsersVersionNeutralController : ControllerBase
{
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) { ... }
}
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
- Familiar controller-based structure

````
