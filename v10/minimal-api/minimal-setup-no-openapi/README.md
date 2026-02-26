# Minimal Setup - No OpenAPI (Minimal API)

This project demonstrates the **absolute minimum** code needed to use `Asp.Versioning` with ASP.NET Core Minimal APIs — **without any OpenAPI integration**.

Compare this directly with `queryheader-versioning/` to see exactly what OpenAPI adds.

## Port

`5003`

## Versioning

Query string: `?api-version=1.0` or `?api-version=2.0`

## What's included

- `Asp.Versioning.Http` — the only required package
- `AddApiVersioning()` — no options needed; `QueryStringApiVersionReader("api-version")` is the default reader
- `NewVersionedApi()` + `MapGroup()` + `HasApiVersion()`

## What's intentionally absent

| Omitted                                                     | Why                                                         |
| ----------------------------------------------------------- | ----------------------------------------------------------- |
| Explicit `ApiVersionReader` config                          | `QueryStringApiVersionReader("api-version")` is the default |
| `Asp.Versioning.Mvc.ApiExplorer`                            | Only needed for OpenAPI document generation                 |
| `Asp.Versioning.OpenApi`                                    | Only needed for v10 OpenAPI integration                     |
| `Microsoft.AspNetCore.OpenApi`                              | Only needed for OpenAPI document generation                 |
| `.AddApiExplorer()`                                         | Only needed for OpenAPI document generation                 |
| `.AddOpenApi()` (on versioning builder)                     | v10-specific OpenAPI chain                                  |
| `app.MapOpenApi()`                                          | Only needed to serve OpenAPI documents                      |
| `ReportApiVersions`                                         | Optional — adds `api-supported-versions` response header    |
| `DefaultApiVersion` / `AssumeDefaultVersionWhenUnspecified` | Optional — requests without `?api-version` return an error  |

## Running

```bash
dotnet run
```

## Testing

Use the `minimal-setup-no-openapi.http` file, or:

```
GET http://localhost:5003/api/users?api-version=1.0
GET http://localhost:5003/api/users?api-version=2.0
```
