# Query Parameter / Header Versioning with Scalar (Controllers)

This project showcases the changes needed to give Scalar a **nice experience** when using query/header-based API versioning with ASP.NET Core Controllers. Specifically, Scalar pre-fills the `api-version` field with the correct value for each version document.

## The "Nice Experience"

The key is `OpenApiOptionsExtensions.ApplyApiVersionDescription`, which adds an `Example` value to the `api-version` parameter schema in each OpenAPI document:

```csharp
builder.Services.AddOpenApi("v1", options =>
{
    options.ApplyApiVersionDescription();
});
```

Inside `ApplyApiVersionDescription`, an operation transformer reads `context.DocumentName` and sets:

```csharp
targetSchema.Example = JsonNode.Parse("\"1.0\""); // for the v1 document
```

Scalar reads the `example` field from the OpenAPI schema and pre-fills the `api-version` query/header input automatically.

## How Scalar is Set Up

```csharp
app.MapScalarApiReference(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    // AddDocuments creates a version-switcher dropdown in Scalar.
    options.AddDocuments(provider.ApiVersionDescriptions.Select(d => d.GroupName));
});
```

## Running

```bash
dotnet run
```

Open http://localhost:5008/scalar to explore the API. Switch between v1 and v2 — notice the `api-version` field is already filled in.

## Example Requests

Use the included `.http` files to test:
- `querystring.http` — Query string versioning
- `header.http` — HTTP header versioning

### Query String
- Users v1: `GET http://localhost:5008/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5008/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5008/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5008/api/scores?api-version=2.0`

### Version-Neutral
- Delete User: `DELETE http://localhost:5008/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5008/openapi/v1.json`
- v2: `GET http://localhost:5008/openapi/v2.json`
