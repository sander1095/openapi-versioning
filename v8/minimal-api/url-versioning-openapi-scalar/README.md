# URL-Based Versioning with Scalar (Minimal APIs)

This project adds Scalar as the API visualization tool for URL-based versioning with ASP.NET Core Minimal APIs. Because the version is already embedded in the URL path, Scalar automatically shows version-specific URLs without any extra configuration.

## Scalar Integration

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

`AddDocument` then gives Scalar a version dropdown to switch between them:

```csharp
app.MapScalarApiReference(options =>
{
    var descriptions = app.DescribeApiVersions();

    for (var i = 0; i < descriptions.Count; i++)
    {
        var description = descriptions[i];
        var isDefault = i == descriptions.Count - 1;

        options.AddDocument(description.GroupName, description.GroupName, isDefault: isDefault);
    }
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
