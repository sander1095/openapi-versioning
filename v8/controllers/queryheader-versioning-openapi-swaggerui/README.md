# Query Parameter / Header Versioning with Swagger UI (Controllers)

This project demonstrates query/header-based API versioning with ASP.NET Core Controllers using `Microsoft.AspNetCore.OpenApi` for document generation and Swagger UI for browsing.

## How It Works

- One OpenAPI document is generated per version (`v1`, `v2`) via `AddOpenApi(...)`.
- `ApplyApiVersionDescription()` sets a version-specific example for the `api-version` parameter.
- Swagger UI uses `app.DescribeApiVersions()` and `/openapi/{group}.json` endpoints.

## Swagger UI Setup

```csharp
app.MapOpenApi();

// ... map controllers first ...

app.UseSwaggerUI(options =>
{
    foreach (var description in app.DescribeApiVersions())
    {
        options.SwaggerEndpoint(
            $"/openapi/{description.GroupName}.json",
            description.GroupName.ToUpperInvariant());
    }
});
```

Keep `UseSwaggerUI(...)` after controller mapping.

## Running

```bash
dotnet run
```

Open http://localhost:5009/swagger to explore the API. Switch between V1 and V2 — notice the `api-version` field is already filled in.

## Example Requests

Use the included `.http` files to test:
- `querystring.http` — Query string versioning
- `header.http` — HTTP header versioning

### Query String
- Users v1: `GET http://localhost:5009/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5009/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5009/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5009/api/scores?api-version=2.0`

### Version-Neutral
- Delete User: `DELETE http://localhost:5009/api/users/{id}`

## OpenAPI Documents

- v1 document: `GET http://localhost:5009/openapi/v1.json`
- v2 document: `GET http://localhost:5009/openapi/v2.json`

## Swagger UI

- Open Swagger UI: http://localhost:5009/swagger
