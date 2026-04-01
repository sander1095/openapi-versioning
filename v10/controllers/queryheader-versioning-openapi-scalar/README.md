# Query Parameter / Header Versioning with Scalar (Controllers)

This project adds Scalar as the API visualization tool for query/header-based versioning with ASP.NET Core Controllers. Scalar pre-fills the `api-version` field with the correct value for each version document, so users do not have to type it manually.

## Scalar Integration

In v10, `Asp.Versioning.OpenApi` automatically sets a `default` value on the `api-version` parameter schema in each generated document. For the `v1` document this looks like:

```json
"schema": {
  "type": "string",
  "default": "1.0"
}
```

Scalar reads this `default` field and pre-fills the `api-version` input automatically — no custom transformer is needed. This contrasts with v8, which required a custom `ApplyApiVersionDescription` extension to add an `example` field.

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
