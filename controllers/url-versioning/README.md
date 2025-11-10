# URL-Based API Versioning (Controllers)

This implementation uses URL segment versioning where the API version is part of the URL path with ASP.NET Core Controllers.

## Project Structure

Controllers are organized by version in separate folders:

```
Controllers/
├── V1/
│   ├── UsersController.cs    (handles /api/v1/users)
│   └── ScoresController.cs   (handles /api/v1/scores)
├── V2/
│   ├── UsersController.cs    (handles /api/v2/users)
│   └── ScoresController.cs   (handles /api/v2/scores)
└── UsersVersionNeutralController.cs  (handles /api/users - no version)
```

## Configuration

```csharp
options.ApiVersionReader = new UrlSegmentApiVersionReader();
options.SubstituteApiVersionInUrl = true;
```

## Controller Implementation

Each version has its own dedicated controller in a namespace folder. Controllers use `[ApiVersion]` to declare their version:

**V1 Controller:**
```csharp
namespace url_versioning.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(1.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<Userv1[]> Get() { ... }
}
```

**V2 Controller:**
```csharp
namespace url_versioning.Controllers.V2;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(2.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<Userv2[]> Get() { ... }
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

## Benefits of This Approach

- **Clear separation**: Each version is isolated in its own folder
- **Easy maintenance**: Version-specific changes don't affect other versions
- **Scalability**: Adding new versions is straightforward
- **Team collaboration**: Different developers can work on different versions
- **Code organization**: Related version code stays together

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
- Clean separation of concerns with folder organization
