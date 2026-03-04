# URL-Based Versioning with Scalar (Controllers)

This project showcases the **nice experience** Scalar provides out-of-the-box with URL-based versioning using ASP.NET Core Controllers. Because the version is part of the URL path, Scalar automatically shows pre-filled, version-specific URLs — no extra parameter configuration required.

## The "Nice Experience"

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
});
```

The v1 OpenAPI document will have paths like `/api/v1/users` and v2 will show `/api/v2/users`. Scalar reads these paths directly — no manual example values needed.

`AddDocuments` gives Scalar a version dropdown to switch between them:

```csharp
app.MapScalarApiReference(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    options.AddDocuments(provider.ApiVersionDescriptions.Select(d => d.GroupName));
});
```

## Running

```bash
dotnet run
```

Open http://localhost:5010/scalar to explore the API. Use the version dropdown — each version shows the correct versioned URL path automatically.

## Example Requests

Use the included `url-scalar-versioning.http` file to test all endpoints.

### URL Examples
- Users v1: `GET http://localhost:5010/api/v1/users`
- Users v2: `GET http://localhost:5010/api/v2/users`
- Scores v1: `GET http://localhost:5010/api/v1/scores`
- Scores v2: `GET http://localhost:5010/api/v2/scores`

### Version-Neutral
- Delete User: `DELETE http://localhost:5010/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5010/openapi/v1.json`
- v2: `GET http://localhost:5010/openapi/v2.json`
