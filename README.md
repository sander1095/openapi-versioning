# API Versioning Examples for ASP.NET Core

This repository demonstrates how API versioning works in ASP.NET Core using both **Minimal APIs** and **Controllers**. It also documents the shift from Swashbuckle/NSwag to `Microsoft.AspNetCore.OpenApi`, and the migration from `Asp.Versioning` v8 to v10.

- **v8/** uses `Asp.Versioning` 8.x with the legacy OpenAPI toolchain.
- **v10/** uses `Asp.Versioning` 10.x with `Microsoft.AspNetCore.OpenApi` 10.x (the new built-in approach).

## 📖 How to Read This Repo

The projects are intentionally ordered to build on each other. Read them in sequence within each folder (`v8/` or `v10/`) to understand **exactly why each line of code exists**:

1. **`minimal-setup-no-openapi`** — Pure versioning, nothing else. No OpenAPI, no extra config. The absolute baseline.
2. **`queryheader-versioning-openapi`** / **`url-versioning-openapi`** — Adds an OpenAPI document on top of the baseline. Shows exactly what OpenAPI integration requires.
3. **`queryheader-versioning-openapi-scalar`** / **`url-versioning-openapi-scalar`** — Adds Scalar as the OpenAPI UI with a polished developer experience.
4. **`queryheader-versioning-openapi-swaggerui`** / **`url-versioning-openapi-swaggerui`** — Same, but with Swagger UI instead of Scalar.

By following this progression, every additional line in each project is motivated by the step before it.

## 📁 Repository Structure

### [v8/](./v8) — `Asp.Versioning` 8.x

- **Minimal APIs** ([v8/minimal-api/](./v8/minimal-api))
  - **[minimal-setup-no-openapi/](./v8/minimal-api/minimal-setup-no-openapi)** — Baseline: versioning only, no OpenAPI (Port 5003)
  - **[queryheader-versioning-openapi/](./v8/minimal-api/queryheader-versioning-openapi)** — Query/header versioning + OpenAPI document (Port 5001)
  - **[url-versioning-openapi/](./v8/minimal-api/url-versioning-openapi)** — URL versioning + OpenAPI document (Port 5000)
  - **[aot-versioning-openapi/](./v8/minimal-api/aot-versioning-openapi)** — Query/header versioning with Native AOT (Port 5002)
  - **[queryheader-versioning-openapi-scalar/](./v8/minimal-api/queryheader-versioning-openapi-scalar)** — Query/header + Scalar UI (Port 5004)
  - **[queryheader-versioning-openapi-swaggerui/](./v8/minimal-api/queryheader-versioning-openapi-swaggerui)** — Query/header + Swagger UI (Port 5005)
  - **[url-versioning-openapi-scalar/](./v8/minimal-api/url-versioning-openapi-scalar)** — URL versioning + Scalar UI (Port 5006)
  - **[url-versioning-openapi-swaggerui/](./v8/minimal-api/url-versioning-openapi-swaggerui)** — URL versioning + Swagger UI (Port 5007)
- **Controllers** ([v8/controllers/](./v8/controllers))
  - **[minimal-setup-no-openapi/](./v8/controllers/minimal-setup-no-openapi)** — Baseline: versioning only, no OpenAPI (Port 5003)
  - **[queryheader-versioning-openapi/](./v8/controllers/queryheader-versioning-openapi)** — Query/header versioning + OpenAPI document (Port 5001)
  - **[url-versioning-openapi/](./v8/controllers/url-versioning-openapi)** — URL versioning + OpenAPI document (Port 5000)
  - **[queryheader-versioning-openapi-scalar/](./v8/controllers/queryheader-versioning-openapi-scalar)** — Query/header + Scalar UI (Port 5008)
  - **[queryheader-versioning-openapi-swaggerui/](./v8/controllers/queryheader-versioning-openapi-swaggerui)** — Query/header + Swagger UI (Port 5009)
  - **[url-versioning-openapi-scalar/](./v8/controllers/url-versioning-openapi-scalar)** — URL versioning + Scalar UI (Port 5010)
  - **[url-versioning-openapi-swaggerui/](./v8/controllers/url-versioning-openapi-swaggerui)** — URL versioning + Swagger UI (Port 5011)

### [v10/](./v10) — `Asp.Versioning` 10.x

- **Minimal APIs** ([v10/minimal-api/](./v10/minimal-api))
  - **[minimal-setup-no-openapi/](./v10/minimal-api/minimal-setup-no-openapi)** — Baseline: versioning only, no OpenAPI (Port 5003)
  - **[queryheader-versioning-openapi/](./v10/minimal-api/queryheader-versioning-openapi)** — Query/header versioning + OpenAPI document (Port 5001)
  - **[url-versioning-openapi/](./v10/minimal-api/url-versioning-openapi)** — URL versioning + OpenAPI document (Port 5000)
  - **[aot-versioning-openapi/](./v10/minimal-api/aot-versioning-openapi)** — Query/header versioning with Native AOT (Port 5002)
- **Controllers** ([v10/controllers/](./v10/controllers))
  - **[minimal-setup-no-openapi/](./v10/controllers/minimal-setup-no-openapi)** — Baseline: versioning only, no OpenAPI (Port 5003)
  - **[queryheader-versioning-openapi/](./v10/controllers/queryheader-versioning-openapi)** — Query/header versioning + OpenAPI document (Port 5001)
  - **[url-versioning-openapi/](./v10/controllers/url-versioning-openapi)** — URL versioning + OpenAPI document (Port 5000)

## 🎯 Versioning Strategies

### URL Segment Versioning
Version is embedded in the URL path: `/api/v1/users`, `/api/v2/users`
- **Benefits**: Clear, cacheable, instantly visible in logs and browser history
- **Configuration**: `UrlSegmentApiVersionReader()`

### Query String / Header Versioning
Version passed via query parameter or HTTP header
- **Query**: `/api/users?api-version=1.0`
- **Header**: `x-api-version: 1.0`
- **Benefits**: Clean URLs, flexible, supports multiple methods simultaneously
- **Configuration**: `QueryStringApiVersionReader()` + `HeaderApiVersionReader()`

## 🚀 Quick Start

```bash
cd v10/minimal-api/minimal-setup-no-openapi
dotnet run
```

## 📊 Minimal API vs Controllers

| Feature             | Minimal APIs                          | Controllers                              |
| ------------------- | ------------------------------------- | ---------------------------------------- |
| **Package**         | `Asp.Versioning.Http`                 | `Asp.Versioning.Mvc`                     |
| **Version declare** | `HasApiVersion(1.0)` on groups        | `[ApiVersion(1.0)]` on controller        |
| **Version map**     | `HasApiVersion()` on groups           | `[MapToApiVersion(1.0)]` on actions      |
| **Version neutral** | `IsApiVersionNeutral()`               | `[ApiVersionNeutral]`                    |
| **Routing**         | `MapGroup()` with route templates     | `[Route]` attributes                     |
| **Endpoints**       | Lambda expressions or local functions | Action methods returning `ActionResult`  |

## 🔧 Build-time OpenAPI Documents

Every OpenAPI-enabled project automatically generates its OpenAPI document(s) at **build time** into `generated-openapi-documents/`

Each subfolder is named after the project's path (version → style → project), and contains one `.json` file per API version. This makes it easy to **diff OpenAPI documents across projects or branches** to see exactly what changed in the API surface.

This is powered by [`Microsoft.Extensions.ApiDescription.Server`](https://www.nuget.org/packages/Microsoft.Extensions.ApiDescription.Server) configured in the root [`Directory.Build.props`](./Directory.Build.props). Projects are opted in by a simple name-based condition: the project name must contain `openapi` but must **not** contain `no-openapi`. This naturally excludes `minimal-setup-no-openapi` (matched by the negative clause).

## �🛠 Technology Stack

- **.NET**: 10.0
- **API Versioning**: `Asp.Versioning` 8.x (v8/) and 10.x (v10/)
- **OpenAPI**: `Microsoft.AspNetCore.OpenApi` 10.x
