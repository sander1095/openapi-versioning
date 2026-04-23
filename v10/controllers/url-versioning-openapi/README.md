# URL-Based API Versioning (Controllers)

This implementation uses URL segment versioning where the API version is part of the URL path with ASP.NET Core Controllers.

## Project Structure

Controllers are organized by version in separate folders for better maintainability:

```
Controllers/
├── V1/
│   ├── UsersController.cs
│   └── ScoresController.cs
├── V2/
│   ├── UsersController.cs
│   └── ScoresController.cs
└── UsersVersionNeutralController.cs
```

## Configuration

Uses `UrlSegmentApiVersionReader()` with `SubstituteApiVersionInUrl = true`. See `Program.cs` for full configuration.

## Implementation Approach

Controllers use:
- `[ApiVersion]` attribute on the controller class to declare the version
- `[Route]` with `{version:apiVersion}` placeholder for versioned routes
- `[ApiVersionNeutral]` for version-independent endpoints
- Namespace folders (V1, V2) to organize versions

Each version is completely isolated in its own folder/namespace, making it easy to maintain and evolve versions independently.

Check the controller classes in the `Controllers/V1/` and `Controllers/V2/` folders to see the full implementation.

## Benefits of Folder Structure

- **Clear separation**: Each version is isolated in its own folder
- **Easy maintenance**: Version-specific changes don't affect other versions
- **Scalability**: Adding new versions is straightforward
- **Team collaboration**: Different developers can work on different versions
- **Code organization**: Related version code stays together

## Running

```bash
dotnet run
```

## Example Requests

Use the included `url-versioning.http` file to test all endpoints.

### URL Examples
- Users v1: `GET http://localhost:5028/api/v1/users`
- Users v2: `GET http://localhost:5028/api/v2/users`
- Scores v1: `GET http://localhost:5028/api/v1/scores`
- Scores v2: `GET http://localhost:5028/api/v2/scores`

### Version-Neutral Endpoint
- Delete User: `DELETE http://localhost:5028/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5028/openapi/v1.json`
- v2: `GET http://localhost:5028/openapi/v2.json`

## Benefits

- Clear and explicit versioning in the URL
- Easy to cache and route
- RESTful approach
- Version is immediately visible in logs and monitoring
- Familiar controller-based structure
- Clean separation of concerns with folder organization
