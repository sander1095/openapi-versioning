# Query Parameter / Header Versioning with Swagger UI (Minimal APIs)

This project demonstrates query/header-based API versioning for minimal APIs, using `Microsoft.AspNetCore.OpenApi` documents and Swagger UI as the viewer.

## How It Works

- One OpenAPI document is generated per API version (`v1`, `v2`) using `AddOpenApi(...)`.
- `ApplyApiVersionDescription()` sets a version-specific example for the `api-version` parameter in each document.
- Swagger UI is configured from `app.DescribeApiVersions()` and points to `/openapi/{group}.json`.

## Swagger UI Setup

```csharp
app.MapOpenApi();

// ... map endpoints first ...

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

Keep `UseSwaggerUI(...)` after endpoint mapping so version descriptions are available.

## Running

```bash
dotnet run
```

Open http://localhost:5005/swagger to explore the API. Switch between V1 and V2 using the version dropdown — notice the `api-version` field is already filled in.

## Example Requests

Use the included `.http` files to test:
- `querystring.http` — Query string versioning
- `header.http` — HTTP header versioning

### Query String
- Users v1: `GET http://localhost:5005/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5005/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5005/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5005/api/scores?api-version=2.0`

### Version-Neutral
- Delete User: `DELETE http://localhost:5005/api/users/{id}`

## OpenAPI Documents

- v1 document: `GET http://localhost:5005/openapi/v1.json`
- v2 document: `GET http://localhost:5005/openapi/v2.json`

## Swagger UI

- Open Swagger UI: http://localhost:5005/swagger
