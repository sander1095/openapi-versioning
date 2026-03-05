# ASP.NET Core API Versioning with Minimal APIs (v8)

This folder contains Minimal API implementations of API versioning using `Asp.Versioning` 8.x.

## 📖 Learning Progression

The projects are ordered intentionally — each one builds on the previous. Read them in sequence to understand **exactly why each line of code exists**:

1. **`minimal-setup-no-openapi`** — Pure versioning, nothing else. No OpenAPI, no extra config. The absolute baseline.
2. **`queryheader-versioning-openapi`** / **`url-versioning-openapi`** — Adds an OpenAPI document. Compare with the baseline to see exactly what OpenAPI integration requires.
3. **`queryheader-versioning-openapi-scalar`** / **`url-versioning-openapi-scalar`** — Adds Scalar as the OpenAPI UI, requiring a bit more configuration.
4. **`queryheader-versioning-openapi-swaggerui`** / **`url-versioning-openapi-swaggerui`** — Same, but with Swagger UI. Slightly different setup from Scalar.

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

### 5. queryheader-versioning-openapi-scalar
Query/header versioning with **Scalar** as the OpenAPI UI. Demonstrates `ApplyApiVersionDescription` pre-filling the `api-version` field in Scalar.
- **Port**: 5004
- **UI**: Scalar at `/scalar`
- [View README](./queryheader-versioning-openapi-scalar/README.md)

### 6. queryheader-versioning-openapi-swaggerui
Query/header versioning with **Swagger UI** (Swashbuckle). Demonstrates `ApiVersionParameterExampleFilter` pre-filling the `api-version` field in Swagger UI.
- **Port**: 5005
- **UI**: Swagger UI at `/swagger`
- [View README](./queryheader-versioning-openapi-swaggerui/README.md)

### 7. url-versioning-openapi-scalar
URL versioning with **Scalar**. Demonstrates how `SubstituteApiVersionInUrl = true` causes Scalar to show pre-filled version-specific URL paths.
- **Port**: 5006
- **UI**: Scalar at `/scalar`
- [View README](./url-versioning-openapi-scalar/README.md)

### 8. url-versioning-openapi-swaggerui
URL versioning with **Swagger UI**. Demonstrates how `SubstituteApiVersionInUrl = true` combined with `ConfigureSwaggerOptions` gives Swagger UI a per-version dropdown with pre-filled URL paths.
- **Port**: 5007
- **UI**: Swagger UI at `/swagger`
- [View README](./url-versioning-openapi-swaggerui/README.md)

## Key Features

Minimal APIs use:
- `NewVersionedApi()` to create versioned API groups
- `MapGroup()` to define route groups
- `HasApiVersion()` to assign versions
- `IsApiVersionNeutral()` for version-independent endpoints
- Lambda expressions or local functions for handlers

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
