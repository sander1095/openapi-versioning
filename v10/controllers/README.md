# ASP.NET Core API Versioning with Controllers

This folder contains controller-based implementations of API versioning strategies using the `Asp.Versioning.Mvc` library.

## Projects

### 1. queryheader-versioning
Query parameter and header-based API versioning using Controllers.
- **Port**: 5001
- **Versioning**: Query string (`?api-version=1.0`) or HTTP header (`x-api-version: 1.0`)
- [View README](./queryheader-versioning/README.md)

### 2. url-versioning
URL segment-based API versioning using Controllers with V1/V2 folder structure.
- **Port**: 5000
- **Versioning**: URL path (`/api/v1/users`)
- **Structure**: Separate folders for each version (V1/, V2/)
- [View README](./url-versioning/README.md)

## Key Features

Controllers use:
- `[ApiVersion]` attribute to declare supported versions
- `[MapToApiVersion]` for action method mapping (query/header versioning)
- `[ApiVersionNeutral]` for version-independent endpoints
- `[Route]` attributes for routing configuration
- Traditional action methods returning `IActionResult`

## Technology Stack

- **.NET**: 10.0 (Preview)
- **API Versioning (v10 folders)**: `Asp.Versioning` 10.0.0-preview
	- `Asp.Versioning.Mvc`
	- `Asp.Versioning.Mvc.ApiExplorer`

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
