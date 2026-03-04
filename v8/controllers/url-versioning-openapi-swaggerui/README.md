# URL-Based Versioning with Swagger UI (Controllers)

This project demonstrates URL segment versioning for ASP.NET Core Controllers using `Microsoft.AspNetCore.OpenApi` for versioned documents and Swagger UI for browsing.

## How It Works

`SubstituteApiVersionInUrl = true` handles URL substitution in versioned route templates:

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    // Replaces {version:apiVersion} in controller route templates with the actual version.
    options.SubstituteApiVersionInUrl = true;
});
```

Swagger UI is configured with one endpoint per version:

```csharp
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

Note: `AddEndpointsApiExplorer` is **not** needed for controllers.

## Running

```bash
dotnet run
```

Open http://localhost:5011/swagger to explore the API. Use the version dropdown — V1 shows `/api/v1/users`, V2 shows `/api/v2/users`. No manual input required.

## Example Requests

Use the included `url-swaggerui-versioning.http` file to test all endpoints.

### URL Examples
- Users v1: `GET http://localhost:5011/api/v1/users`
- Users v2: `GET http://localhost:5011/api/v2/users`
- Scores v1: `GET http://localhost:5011/api/v1/scores`
- Scores v2: `GET http://localhost:5011/api/v2/scores`

### Version-Neutral
- Delete User: `DELETE http://localhost:5011/api/users/{id}`

## OpenAPI Documents

- v1 document: `GET http://localhost:5011/openapi/v1.json`
- v2 document: `GET http://localhost:5011/openapi/v2.json`

## Swagger UI

- Open Swagger UI: http://localhost:5011/swagger
