# Minimal Setup - No OpenAPI (Controllers)

This project demonstrates the **absolute minimum** code needed to use `Asp.Versioning` with ASP.NET Core controllers — **without any OpenAPI integration**.

Compare this directly with `queryheader-versioning-openapi/` to see exactly what OpenAPI adds.

## Port

`5003`

## Versioning

Query string: `?api-version=1.0` or `?api-version=2.0`

## What's included

- `Asp.Versioning.Mvc` — the only required package
- `AddApiVersioning()` — no options needed; `QueryStringApiVersionReader("api-version")` is the default reader
- `.AddMvc()` — registers the version-aware action selector (required for `[MapToApiVersion]` to resolve correctly)
- A single `UsersController` declaring both `[ApiVersion(1.0)]` and `[ApiVersion(2.0)]`

## What's intentionally absent

| Omitted                                                     | Why                                                         |
| ----------------------------------------------------------- | ----------------------------------------------------------- |
| Explicit `ApiVersionReader` config                          | `QueryStringApiVersionReader("api-version")` is the default |
| `Asp.Versioning.Mvc.ApiExplorer`                            | Only needed for OpenAPI document generation                 |
| `Asp.Versioning.OpenApi`                                    | Only needed for v10 OpenAPI integration                     |
| `Microsoft.AspNetCore.OpenApi`                              | Only needed for OpenAPI document generation                 |
| `.AddApiExplorer()`                                         | Only needed for OpenAPI document generation                 |
| `.AddMvc().AddOpenApi()`                                    | v10-specific OpenAPI chain                                  |
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
