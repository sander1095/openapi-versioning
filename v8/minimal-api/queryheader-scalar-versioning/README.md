# Query Parameter / Header Versioning with Scalar (Minimal APIs)

This project showcases the changes needed to give Scalar a **nice experience** when using query/header-based API versioning. Specifically, Scalar pre-fills the `api-version` field with the correct value for each API version document, so users do not have to type it manually.

## The "Nice Experience"

The key is `OpenApiOptionsExtensions.ApplyApiVersionDescription`, which adds an `Example` value to the `api-version` parameter schema in each OpenAPI document:

```csharp
builder.Services.AddOpenApi("v1", options =>
{
    options.ApplyApiVersionDescription();
});
```

Inside `ApplyApiVersionDescription`, an operation transformer reads `context.DocumentName` to determine the current version and sets:

```csharp
targetSchema.Example = JsonNode.Parse("\"1.0\""); // for the v1 document
```

Scalar reads the `example` field from the OpenAPI schema and pre-fills the `api-version` query/header input automatically. Without this, users see an empty field and need to know which version to type.

## How Scalar is Set Up

```csharp
app.MapScalarApiReference(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    // AddDocuments registers all versions so Scalar shows a version-switcher dropdown.
    options.AddDocuments(provider.ApiVersionDescriptions.Select(d => d.GroupName));
});
```

`AddDocuments` is what creates the version dropdown in Scalar's UI. Each document has the `api-version` field pre-filled with its version value thanks to `ApplyApiVersionDescription`.

## Running

```bash
dotnet run
```

Open http://localhost:5004/scalar to explore the API. Switch between v1 and v2 using the version dropdown — notice the `api-version` field is already filled in.

## Example Requests

Use the included `.http` files to test:
- `querystring.http` — Query string versioning
- `header.http` — HTTP header versioning

### Query String
- Users v1: `GET http://localhost:5004/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5004/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5004/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5004/api/scores?api-version=2.0`

### Version-Neutral
- Delete User: `DELETE http://localhost:5004/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5004/openapi/v1.json`
- v2: `GET http://localhost:5004/openapi/v2.json`
