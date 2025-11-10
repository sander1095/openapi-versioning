# API Versioning Examples for ASP.NET Core

This repository demonstrates different API versioning strategies in ASP.NET Core 10 using both **Minimal APIs** and **Controllers**.

## 📁 Repository Structure

### [minimal-api/](./minimal-api)
Minimal API implementations using `Asp.Versioning.Http`
- **[queryheader-versioning/](./minimal-api/queryheader-versioning)** - Query string or HTTP header versioning (Port 5001)
- **[url-versioning/](./minimal-api/url-versioning)** - URL segment versioning (Port 5000)

### [controllers/](./controllers)
Controller-based implementations using `Asp.Versioning.Mvc`
- **[queryheader-versioning/](./controllers/queryheader-versioning)** - Query string or HTTP header versioning (Port 5001)
- **[url-versioning/](./controllers/url-versioning)** - URL segment versioning with V1/V2 folder structure (Port 5000)

## 🎯 Versioning Strategies

### 1. URL Segment Versioning
Version is embedded in the URL path: `/api/v1/users`, `/api/v2/users`
- **Port**: 5000
- **Benefits**: Clear, cacheable, RESTful
- **Configuration**: `UrlSegmentApiVersionReader()`

### 2. Query String / Header Versioning
Version passed via query parameter or HTTP header
- **Port**: 5001
- **Query**: `/api/users?api-version=1.0`
- **Header**: `x-api-version: 1.0`
- **Benefits**: Clean URLs, flexible, supports multiple methods
- **Configuration**: `QueryStringApiVersionReader()` or `HeaderApiVersionReader()`

## 🚀 Quick Start

Navigate to any project and run:

```bash
# Minimal API - Query/Header versioning
cd minimal-api/queryheader-versioning
dotnet run

# Minimal API - URL versioning
cd minimal-api/url-versioning
dotnet run

# Controllers - Query/Header versioning
cd controllers/queryheader-versioning
dotnet run

# Controllers - URL versioning
cd controllers/url-versioning
dotnet run
```

## 📊 Implementation Comparison

| Feature | Minimal APIs | Controllers |
|---------|-------------|-------------|
| **Package** | `Asp.Versioning.Http` | `Asp.Versioning.Mvc` |
| **Version Declaration** | `HasApiVersion(1.0)` on groups | `[ApiVersion(1.0)]` on controller |
| **Version Mapping** | `HasApiVersion()` on groups | `[MapToApiVersion(1.0)]` on actions |
| **Version Neutral** | `IsApiVersionNeutral()` | `[ApiVersionNeutral]` |
| **Routing** | `MapGroup()` with route templates | `[Route]` attributes |
| **Endpoints** | Lambda expressions or local functions | Action methods returning `IActionResult` |

## 🧪 Testing

Each project includes:
- `.http` files for REST Client (VS Code) or Visual Studio
- OpenAPI documents at `/openapi/v1.json` and `/openapi/v2.json`
- Identical endpoint behavior across both implementations

### Example Endpoints

**URL Versioning (Port 5000):**
```
GET http://localhost:5000/api/v1/users
GET http://localhost:5000/api/v2/users
GET http://localhost:5000/api/v1/scores
GET http://localhost:5000/api/v2/scores
DELETE http://localhost:5000/api/users/1  (version-neutral)
```

**Query/Header Versioning (Port 5001):**
```
GET http://localhost:5001/api/users?api-version=1.0
GET http://localhost:5001/api/users?api-version=2.0
GET http://localhost:5001/api/scores?api-version=1.0
GET http://localhost:5001/api/scores?api-version=2.0
DELETE http://localhost:5001/api/users/1  (version-neutral)
```

## 🛠 Technology Stack

- **.NET**: 10.0 (Preview)
- **API Versioning**: 8.1.0
  - `Asp.Versioning.Http` (Minimal APIs)
  - `Asp.Versioning.Mvc` (Controllers)
  - `Asp.Versioning.Mvc.ApiExplorer` (OpenAPI integration)
- **OpenAPI**: Microsoft.AspNetCore.OpenApi 10.0.0-rc.2

## 📚 API Versions

All projects implement:
- **Users API**: v1 (Id, Name, Email) → v2 (adds BirthDate)
- **Scores API**: v1 (UserId, Score) → v2 (adds AchievedOn timestamp)
- **Version-neutral DELETE**: Works with any or no version

## 📖 Learn More

- [ASP.NET API Versioning Wiki](https://github.com/dotnet/aspnet-api-versioning/wiki)
- [API Versioning in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/versioning)
- [Minimal APIs Overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
