# ASP.NET Core API Versioning with Controllers (v10)

This folder contains controller-based implementations of API versioning using `Asp.Versioning` 10.x and `Microsoft.AspNetCore.OpenApi` 10.x — the new built-in OpenAPI stack that replaces Swashbuckle/NSwag.

## 📖 Learning Progression

The projects are ordered intentionally — each one builds on the previous. Read them in sequence to understand **exactly why each line of code exists**:

1. **`minimal-setup-no-openapi`** — Pure versioning, nothing else. No OpenAPI, no extra config. The absolute baseline.
2. **`queryheader-versioning-openapi`** / **`url-versioning-openapi`** — Adds an OpenAPI document. Compare with the baseline to see exactly what OpenAPI integration requires in v10.

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

## Key Features

Controllers use:
- `[ApiVersion]` attribute to declare supported versions
- `[MapToApiVersion]` for action method mapping (query/header versioning)
- `[ApiVersionNeutral]` for version-independent endpoints
- `[Route]` attributes for routing configuration
- Traditional action methods returning `IActionResult`

## Technology Stack

- **.NET**: 10.0
- **API Versioning**: `Asp.Versioning` 10.x
  - `Asp.Versioning.Mvc`
  - `Asp.Versioning.OpenApi` (replaces `Asp.Versioning.Mvc.ApiExplorer`)

## Running

```bash
cd queryheader-versioning-openapi
dotnet run

# Or
cd url-versioning-openapi
dotnet run
```

## Learn More

- [ASP.NET API Versioning Wiki](https://github.com/dotnet/aspnet-api-versioning/wiki)
- [API Versioning in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/versioning)
