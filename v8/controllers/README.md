# ASP.NET Core API Versioning with Controllers (v8)

This folder contains controller-based implementations of API versioning using `Asp.Versioning` 8.x.

## 📖 Learning Progression

The projects are ordered intentionally — each one builds on the previous. Read them in sequence to understand **exactly why each line of code exists**:

1. **`minimal-setup-no-openapi`** — Pure versioning, nothing else. No OpenAPI, no extra config. The absolute baseline.
2. **`queryheader-versioning-openapi`** / **`url-versioning-openapi`** — Adds an OpenAPI document. Compare with the baseline to see exactly what OpenAPI integration requires.
3. **`queryheader-versioning-openapi-scalar`** / **`url-versioning-openapi-scalar`** — Adds Scalar as the OpenAPI UI, requiring a bit more configuration.
4. **`queryheader-versioning-openapi-swaggerui`** / **`url-versioning-openapi-swaggerui`** — Same, but with Swagger UI. Slightly different setup from Scalar.

## Projects

### 1. minimal-setup-no-openapi
Absolute minimum setup for `Asp.Versioning` with controllers — no OpenAPI integration.
- **Port**: 5003
- **Versioning**: Query string (`?api-version=1.0`)
- [View README](./minimal-setup-no-openapi/README.md)

### 2. queryheader-versioning-openapi
Query parameter and header-based API versioning with an OpenAPI document.
- **Port**: 5001
- **Versioning**: Query string (`?api-version=1.0`) or HTTP header (`x-api-version: 1.0`)
- [View README](./queryheader-versioning-openapi/README.md)

### 3. url-versioning-openapi
URL segment-based API versioning with an OpenAPI document. Controllers are organized in V1/V2 subfolders.
- **Port**: 5000
- **Versioning**: URL path (`/api/v1/users`)
- [View README](./url-versioning-openapi/README.md)

### 4. queryheader-versioning-openapi-scalar
Query/header versioning with **Scalar** as the OpenAPI UI. Demonstrates `ApplyApiVersionDescription` pre-filling the `api-version` field in Scalar.
- **Port**: 5008
- **UI**: Scalar at `/scalar`
- [View README](./queryheader-versioning-openapi-scalar/README.md)

### 5. queryheader-versioning-openapi-swaggerui
Query/header versioning with **Swagger UI** (Swashbuckle). Demonstrates `ApiVersionParameterExampleFilter` pre-filling the `api-version` field in Swagger UI.
- **Port**: 5009
- **UI**: Swagger UI at `/swagger`
- [View README](./queryheader-versioning-openapi-swaggerui/README.md)

### 6. url-versioning-openapi-scalar
URL versioning with **Scalar**. Demonstrates how `SubstituteApiVersionInUrl = true` causes Scalar to show pre-filled version-specific URL paths.
- **Port**: 5010
- **UI**: Scalar at `/scalar`
- [View README](./url-versioning-openapi-scalar/README.md)

### 7. url-versioning-openapi-swaggerui
URL versioning with **Swagger UI**. Demonstrates how `SubstituteApiVersionInUrl = true` combined with `ConfigureSwaggerOptions` gives Swagger UI a per-version dropdown with pre-filled URL paths.
- **Port**: 5011
- **UI**: Swagger UI at `/swagger`
- [View README](./url-versioning-openapi-swaggerui/README.md)

## Key Features

Controllers use:
- `[ApiVersion]` attribute to declare supported versions
- `[MapToApiVersion]` for action method mapping (query/header versioning)
- `[ApiVersionNeutral]` for version-independent endpoints
- `[Route]` attributes for routing configuration
- Traditional action methods returning `IActionResult`

## Running

Call `dotnet run` in any project folder to start that API.

## Learn More

- [ASP.NET API Versioning Wiki](https://github.com/dotnet/aspnet-api-versioning/wiki)
- [API Versioning in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/versioning)
