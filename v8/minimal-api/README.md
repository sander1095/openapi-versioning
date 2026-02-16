# ASP.NET Core API Versioning with Minimal APIs

This folder contains Minimal API implementations of API versioning strategies using the `Asp.Versioning.Http` library.

## Projects

### 1. queryheader-versioning
Query parameter and header-based API versioning using Minimal APIs.
- **Port**: 5001
- **Versioning**: Query string (`?api-version=1.0`) or HTTP header (`x-api-version: 1.0`)
- [View README](./queryheader-versioning/README.md)

### 2. url-versioning
URL segment-based API versioning using Minimal APIs.
- **Port**: 5000
- **Versioning**: URL path (`/api/v1/users`)
- [View README](./url-versioning/README.md)

## Key Features

Minimal APIs use:
- `NewVersionedApi()` to create versioned API groups
- `MapGroup()` to define route groups
- `HasApiVersion()` to assign versions
- `IsApiVersionNeutral()` for version-independent endpoints
- Lambda expressions or local functions for handlers

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
- [Minimal APIs Overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
