# ASP.NET Core API Versioning with Minimal APIs (v10)

This folder contains Minimal API implementations of API versioning using `Asp.Versioning` 10.x and `Microsoft.AspNetCore.OpenApi` 10.x — the new built-in OpenAPI stack that replaces Swashbuckle/NSwag.

## 📖 Learning Progression

The projects are ordered intentionally — each one builds on the previous. Read them in sequence to understand **exactly why each line of code exists**:

1. **`minimal-setup-no-openapi`** — Pure versioning, nothing else. No OpenAPI, no extra config. The absolute baseline.
2. **`queryheader-versioning-openapi`** / **`url-versioning-openapi`** — Adds an OpenAPI document. Compare with the baseline to see exactly what OpenAPI integration requires in v10.

`aot-versioning-openapi` is a side project that demonstrates Native AOT compilation alongside query/header versioning.

## Projects

### 1. minimal-setup-no-openapi
Absolute minimum setup for `Asp.Versioning` with Minimal APIs — no OpenAPI integration.
- **Port**: 5003
- **Versioning**: Query string (`?api-version=1.0`)
- [View README](./minimal-setup-no-openapi/README.md)

### 2. queryheader-versioning-openapi
Query parameter and header-based API versioning with an OpenAPI document.
- **Port**: 5001
- **Versioning**: Query string (`?api-version=1.0`) or HTTP header (`x-api-version: 1.0`)
- [View README](./queryheader-versioning-openapi/README.md)

### 3. url-versioning-openapi
URL segment-based API versioning with an OpenAPI document.
- **Port**: 5000
- **Versioning**: URL path (`/api/v1/users`)
- [View README](./url-versioning-openapi/README.md)

### 4. aot-versioning-openapi
Query/header versioning optimized for **Native AOT** compilation.
- **Port**: 5002
- **Versioning**: Query string or HTTP header
- [View README](./aot-versioning-openapi/README.md)

## Key Features

Minimal APIs use:
- `NewVersionedApi()` to create versioned API groups
- `MapGroup()` to define route groups
- `HasApiVersion()` to assign versions
- `IsApiVersionNeutral()` for version-independent endpoints
- Lambda expressions or local functions for handlers

## Technology Stack

- **.NET**: 10.0
- **API Versioning**: `Asp.Versioning` 10.x
  - `Asp.Versioning.Http`
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
- [Minimal APIs Overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
