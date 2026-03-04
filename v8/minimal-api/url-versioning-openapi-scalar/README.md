# URL-Based Versioning with Scalar (Minimal APIs)

This project showcases the **nice experience** Scalar provides out-of-the-box with URL-based versioning. Because the version is part of the URL path, Scalar automatically shows pre-filled, version-specific URLs — no extra parameter configuration required.

## The "Nice Experience"

With URL versioning, the key setting is `SubstituteApiVersionInUrl = true`:

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    // When true, the {version:apiVersion} placeholder in routes is replaced
    // with the actual version value in the generated OpenAPI document.
    // So the v1 document shows /api/v1/users instead of /api/{version}/users.
    options.SubstituteApiVersionInUrl = true;
});
```

The v1 OpenAPI document will have paths like `/api/v1/users` and the v2 document will show `/api/v2/users`. Scalar reads these paths directly — no manual example values needed.

`AddDocuments` then gives Scalar a version dropdown to switch between them:

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

Open http://localhost:5006/scalar to explore the API. Use the version dropdown to switch between v1 and v2 — each shows the correct versioned URL path automatically.

## Example Requests

Use the included `url-scalar-versioning.http` file to test all endpoints.

### URL Examples
- Users v1: `GET http://localhost:5006/api/v1/users`
- Users v2: `GET http://localhost:5006/api/v2/users`
- Scores v1: `GET http://localhost:5006/api/v1/scores`
- Scores v2: `GET http://localhost:5006/api/v2/scores`

### Version-Neutral
- Delete User: `DELETE http://localhost:5006/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5006/openapi/v1.json`
- v2: `GET http://localhost:5006/openapi/v2.json`
