# ASP.NET Core API Versioning with Controllers

This folder contains controller-based implementations of API versioning strategies using the `Asp.Versioning.Mvc` library.

## Projects

### 1. minimal-setup-no-openapi
Absolute minimum setup for `Asp.Versioning` with controllers — no OpenAPI integration.
- **Port**: 5003
- **Versioning**: Query string (`?api-version=1.0`)
- [View README](./minimal-setup-no-openapi/README.md)

### 2. queryheader-versioning
Query parameter and header-based API versioning using Controllers.
- **Port**: 5001
- **Versioning**: Query string (`?api-version=1.0`) or HTTP header (`x-api-version: 1.0`)
- [View README](./queryheader-versioning/README.md)

### 3. url-versioning
URL segment-based API versioning using Controllers with V1/V2 folder structure.
- **Port**: 5000
- **Versioning**: URL path (`/api/v1/users`)
- **Structure**: Separate folders for each version (V1/, V2/)
- [View README](./url-versioning/README.md)

### 4. queryheader-scalar-versioning
Query/header versioning with **Scalar** as the OpenAPI UI. Demonstrates `ApplyApiVersionDescription` pre-filling the `api-version` field in Scalar.
- **Port**: 5008
- **UI**: Scalar at `/scalar`
- [View README](./queryheader-scalar-versioning/README.md)

### 5. queryheader-swaggerui-versioning
Query/header versioning with **Swagger UI** (Swashbuckle). Demonstrates `ApiVersionParameterExampleFilter` pre-filling the `api-version` field in Swagger UI.
- **Port**: 5009
- **UI**: Swagger UI at `/swagger`
- [View README](./queryheader-swaggerui-versioning/README.md)

### 6. url-scalar-versioning
URL versioning with **Scalar** as the OpenAPI UI. Demonstrates how `SubstituteApiVersionInUrl = true` causes Scalar to show pre-filled version-specific URL paths.
- **Port**: 5010
- **UI**: Scalar at `/scalar`
- [View README](./url-scalar-versioning/README.md)

### 7. url-swaggerui-versioning
URL versioning with **Swagger UI** (Swashbuckle). Demonstrates how `SubstituteApiVersionInUrl = true` combined with `ConfigureSwaggerOptions` gives Swagger UI a per-version dropdown with pre-filled URL paths.
- **Port**: 5011
- **UI**: Swagger UI at `/swagger`
- [View README](./url-swaggerui-versioning/README.md)

## Key Features

Controllers use:
- `[ApiVersion]` attribute to declare supported versions
- `[MapToApiVersion]` for action method mapping (query/header versioning)
- `[ApiVersionNeutral]` for version-independent endpoints
- `[Route]` attributes for routing configuration
- Traditional action methods returning `IActionResult`

## Differences from Minimal APIs

| Feature                 | Controllers          | Minimal APIs          |
| ----------------------- | -------------------- | --------------------- |
| **Package**             | `Asp.Versioning.Mvc` | `Asp.Versioning.Http` |
| **Routing**             | `[Route]` attributes | `MapGroup()`          |
| **Version Declaration** | `[ApiVersion]`       | `HasApiVersion()`     |
| **Endpoints**           | Action methods       | Lambda expressions    |

## Running

```bash
cd queryheader-versioning
dotnet run

# Or
cd url-versioning
dotnet run
```

## Learn More

- [ASP.NET API Versioning Wiki](https://github.com/dotnet/aspnet-api-versioning/wiki)
- [API Versioning in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/versioning)
