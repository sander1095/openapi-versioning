# Query Parameter / Header Versioning with Scalar (Controllers)

This project adds Scalar as the API visualization tool for query/header-based versioning with ASP.NET Core Controllers. The generated OpenAPI document now constrains `api-version` to the single valid value for each version document, so no custom transformer is needed.

## Scalar Integration

In v10, `Asp.Versioning.OpenApi` automatically limits the `api-version` parameter schema to the correct version for each generated document. For the `v1` document this now looks like:

```json
"schema": {
  "enum": [
    "1.0"
  ],
  "type": "string"
}
```

Scalar can use this generated schema directly — no custom transformer is needed. This contrasts with v8, which required a custom `ApplyApiVersionDescription` extension to add an `example` field.

`AddDocument` creates a version-switcher dropdown in Scalar:

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

Open http://localhost:5027/scalar to explore the API. Switch between v1 and v2 to see each document's version-specific schema.

## Example Requests

Use the included `.http` files to test:
- `querystring.http` — Query string versioning
- `header.http` — HTTP header versioning

### Query String
- Users v1: `GET http://localhost:5027/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5027/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5027/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5027/api/scores?api-version=2.0`

### Version-Neutral
- Delete User: `DELETE http://localhost:5027/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5027/openapi/v1.json`
- v2: `GET http://localhost:5027/openapi/v2.json`
