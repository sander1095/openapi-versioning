# Query Parameter / Header Versioning with Swagger UI (Minimal APIs)

This project adds Swagger UI as the API visualization tool for query/header-based versioning with ASP.NET Core Minimal APIs. Swagger UI renders a version-switcher dropdown so users can browse each API version's documentation.

## Swagger UI Setup

```csharp
app.MapOpenApi().WithDocumentPerVersion();

// ... map endpoints first ...

// UseSwaggerUI MUST come after MapOpenApi() and the API endpoint definitions,
// possibly due to the use of DescribeApiVersions()
app.UseSwaggerUI(options =>
{
    foreach (var description in app.DescribeApiVersions().Reverse())
    {
        options.SwaggerEndpoint(
            $"/openapi/{description.GroupName}.json",
            description.GroupName.ToUpperInvariant());
    }
});
```

`WithDocumentPerVersion()` exposes a separate `/openapi/{version}.json` endpoint for each API version. `UseSwaggerUI` reads these endpoints and renders a version-switcher dropdown.

## Running

```bash
dotnet run
```

Open http://localhost:5015/swagger to explore the API. Switch between V1 and V2 using the version dropdown.

## Example Requests

Use the included `.http` files to test:
- `querystring.http` — Query string versioning
- `header.http` — HTTP header versioning

### Query String
- Users v1: `GET http://localhost:5015/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5015/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5015/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5015/api/scores?api-version=2.0`

### Version-Neutral
- Delete User: `DELETE http://localhost:5015/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5015/openapi/v1.json`
- v2: `GET http://localhost:5015/openapi/v2.json`

## Swagger UI

- Open Swagger UI: http://localhost:5015/swagger
