# API Versioning Examples

This solution demonstrates different API versioning strategies in ASP.NET Core 10 Minimal APIs.

## Projects

### 1. **url-versioning** - URL Segment Versioning
API version is embedded in the URL path (e.g., `/api/v1/users`, `/api/v2/users`)

- Port: `http://localhost:5000`
- [See url-versioning/README.md](url-versioning/README.md)

### 2. **queryheader-versioning** - Query Parameter or Header Versioning
API version is passed via query string (e.g., `/api/users?api-version=1.0`) or HTTP header

- Port: `http://localhost:5001`
- [See queryheader-versioning/README.md](queryheader-versioning/README.md)

## Running the Examples

Navigate to either project directory and run:

```bash
cd url-versioning
dotnet run
```

Or:

```bash
cd queryheader-versioning
dotnet run
```

Or run both from the solution file:

```bash
dotnet build
```

## Key Differences

| Feature | URL-Based | Query/Header-Based |
|---------|-----------|-------------------|
| URL Pattern | `/api/v{version}/resource` | `/api/resource?api-version=1.0` |
| Configuration | `UrlSegmentApiVersionReader` | `QueryStringApiVersionReader` or `HeaderApiVersionReader` |
| URL Clarity | ✅ Version visible in URL | ❌ Version in query/header |
| Caching | ✅ Easy to cache | ⚠️ Requires cache-key consideration |
| Flexibility | ⚠️ URL structure changes | ✅ Same URL for all versions |

## Technology Stack

- .NET 10.0
- ASP.NET Core Minimal APIs
- Asp.Versioning.Http 8.1.0
- Asp.Versioning.Mvc.ApiExplorer 8.1.0
- Microsoft.AspNetCore.OpenApi (RC)

## Testing the APIs

Both projects expose OpenAPI specifications:

- **URL Versioning**: 
  - v1: http://localhost:5000/openapi/v1.json
  - v2: http://localhost:5000/openapi/v2.json

- **Query/Header Versioning**:
  - v1: http://localhost:5001/openapi/v1.json
  - v2: http://localhost:5001/openapi/v2.json
