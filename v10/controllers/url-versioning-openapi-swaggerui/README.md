# URL-Based Versioning with Swagger UI (Controllers)

This project adds Swagger UI as the API visualization tool for URL-based versioning with ASP.NET Core Controllers. Because the version is already embedded in the URL path, Swagger UI automatically shows version-specific URLs without any extra configuration.

## Swagger UI Setup

With URL versioning, the key setting is `SubstituteApiVersionInUrl = true`:

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    // When true, the {version:apiVersion} placeholder in controller routes is replaced
    // with the actual version value in the generated OpenAPI document.
    options.SubstituteApiVersionInUrl = true;
})
.AddMvc()
.AddOpenApi();
```

The v1 OpenAPI document will have paths like `/api/v1/users` and v2 will show `/api/v2/users`. Swagger UI reads these paths directly — no manual example values needed.

`WithDocumentPerVersion()` exposes a separate `/openapi/{version}.json` endpoint per version, and `UseSwaggerUI` registers each one as a Swagger endpoint:

```csharp
app.MapOpenApi().WithDocumentPerVersion();

app.MapControllers();

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

## Running

```bash
dotnet run
```

Open http://localhost:5014/swagger to explore the API. Use the version dropdown — V1 shows `/api/v1/users`, V2 shows `/api/v2/users`. No manual input required.

## Example Requests

Use the included `url-swaggerui-versioning.http` file to test all endpoints.

### URL Examples
- Users v1: `GET http://localhost:5014/api/v1/users`
- Users v2: `GET http://localhost:5014/api/v2/users`
- Scores v1: `GET http://localhost:5014/api/v1/scores`
- Scores v2: `GET http://localhost:5014/api/v2/scores`

### Version-Neutral
- Delete User: `DELETE http://localhost:5014/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5014/openapi/v1.json`
- v2: `GET http://localhost:5014/openapi/v2.json`

## Swagger UI

- Open Swagger UI: http://localhost:5014/swagger
