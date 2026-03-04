# API Versioning Examples for ASP.NET Core

This repository demonstrates different API versioning strategies in ASP.NET Core 10 using both **Minimal APIs** and **Controllers**.

- **v8/** shows the legacy/previous approach.
- **v10/** is the workspace for the new v10 approach.

## 📁 Repository Structure

### [v8/](./v8) (legacy)
Legacy implementations used to show how things were done previously.

- **Minimal APIs** ([v8/minimal-api/](./v8/minimal-api))
  - **[minimal-setup-no-openapi/](./v8/minimal-api/minimal-setup-no-openapi)** - Minimum viable versioning, no OpenAPI (Port 5003)
  - **[queryheader-versioning/](./v8/minimal-api/queryheader-versioning)** - Query string or HTTP header versioning with Scalar + Swagger UI (Port 5001)
  - **[url-versioning/](./v8/minimal-api/url-versioning)** - URL segment versioning with Scalar (Port 5000)
  - **[aot-versioning/](./v8/minimal-api/aot-versioning)** - AOT-optimized query/header versioning (Port 5002)
  - **[queryheader-scalar-versioning/](./v8/minimal-api/queryheader-scalar-versioning)** - Query/header versioning focused on Scalar nice experience (Port 5004)
  - **[queryheader-swaggerui-versioning/](./v8/minimal-api/queryheader-swaggerui-versioning)** - Query/header versioning focused on Swagger UI nice experience (Port 5005)
  - **[url-scalar-versioning/](./v8/minimal-api/url-scalar-versioning)** - URL versioning focused on Scalar nice experience (Port 5006)
  - **[url-swaggerui-versioning/](./v8/minimal-api/url-swaggerui-versioning)** - URL versioning focused on Swagger UI nice experience (Port 5007)
- **Controllers** ([v8/controllers/](./v8/controllers))
  - **[minimal-setup-no-openapi/](./v8/controllers/minimal-setup-no-openapi)** - Minimum viable versioning, no OpenAPI (Port 5003)
  - **[queryheader-versioning/](./v8/controllers/queryheader-versioning)** - Query string or HTTP header versioning (Port 5001)
  - **[url-versioning/](./v8/controllers/url-versioning)** - URL segment versioning with V1/V2 folder structure (Port 5000)
  - **[queryheader-scalar-versioning/](./v8/controllers/queryheader-scalar-versioning)** - Query/header versioning focused on Scalar nice experience (Port 5008)
  - **[queryheader-swaggerui-versioning/](./v8/controllers/queryheader-swaggerui-versioning)** - Query/header versioning focused on Swagger UI nice experience (Port 5009)
  - **[url-scalar-versioning/](./v8/controllers/url-scalar-versioning)** - URL versioning focused on Scalar nice experience (Port 5010)
  - **[url-swaggerui-versioning/](./v8/controllers/url-swaggerui-versioning)** - URL versioning focused on Swagger UI nice experience (Port 5011)

### [v10/](./v10) (new approach)
New v10 workspace with the same project structure, intended for the updated versioning library approach.

- **Minimal APIs** ([v10/minimal-api/](./v10/minimal-api))
  - **[minimal-setup-no-openapi/](./v10/minimal-api/minimal-setup-no-openapi)** - Minimum viable versioning, no OpenAPI (Port 5003)
  - **[queryheader-versioning/](./v10/minimal-api/queryheader-versioning)**
  - **[url-versioning/](./v10/minimal-api/url-versioning)**
  - **[aot-versioning/](./v10/minimal-api/aot-versioning)**
- **Controllers** ([v10/controllers/](./v10/controllers))
  - **[minimal-setup-no-openapi/](./v10/controllers/minimal-setup-no-openapi)** - Minimum viable versioning, no OpenAPI (Port 5003)
  - **[queryheader-versioning/](./v10/controllers/queryheader-versioning)**
  - **[url-versioning/](./v10/controllers/url-versioning)**

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
# v8 (legacy) - Minimal API - No OpenAPI (minimum viable)
cd v8/minimal-api/minimal-setup-no-openapi
dotnet run

# v8 (legacy) - Minimal API - Query/Header versioning
cd v8/minimal-api/queryheader-versioning
dotnet run

# v8 (legacy) - Minimal API - URL versioning
cd v8/minimal-api/url-versioning
dotnet run

# v8 (legacy) - Minimal API - AOT-optimized Query/Header versioning
cd v8/minimal-api/aot-versioning
dotnet run

# v8 (legacy) - Controllers - Query/Header versioning
cd v8/controllers/queryheader-versioning
dotnet run

# v8 (legacy) - Controllers - No OpenAPI (minimum viable)
cd v8/controllers/minimal-setup-no-openapi
dotnet run

# v8 (legacy) - Controllers - URL versioning
cd v8/controllers/url-versioning
dotnet run

# v10 (new approach workspace) - Controllers - URL versioning
cd v10/controllers/url-versioning
dotnet run
```

## 📊 Implementation Comparison

| Feature                 | Minimal APIs                          | Controllers                              |
| ----------------------- | ------------------------------------- | ---------------------------------------- |
| **Package**             | `Asp.Versioning.Http`                 | `Asp.Versioning.Mvc`                     |
| **Version Declaration** | `HasApiVersion(1.0)` on groups        | `[ApiVersion(1.0)]` on controller        |
| **Version Mapping**     | `HasApiVersion()` on groups           | `[MapToApiVersion(1.0)]` on actions      |
| **Version Neutral**     | `IsApiVersionNeutral()`               | `[ApiVersionNeutral]`                    |
| **Routing**             | `MapGroup()` with route templates     | `[Route]` attributes                     |
| **Endpoints**           | Lambda expressions or local functions | Action methods returning `IActionResult` |

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
- **API Versioning**:
  - **v8 folders**: 8.1.0
    - `Asp.Versioning.Http` (Minimal APIs)
    - `Asp.Versioning.Mvc` (Controllers)
    - `Asp.Versioning.Mvc.ApiExplorer` (OpenAPI integration)
  - **v10 folders**: 10.0.0-preview
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
