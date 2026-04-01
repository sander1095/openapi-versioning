# Query Parameter / Header Versioning with Scalar (Minimal APIs)

This project adds Scalar as the API visualization tool for query/header-based versioning with ASP.NET Core Minimal APIs. Scalar renders a version-switcher dropdown so users can browse each API version's documentation.

## How Scalar is Set Up

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

`AddDocument` creates a version-switcher dropdown in Scalar so users can switch between v1 and v2.

## Running

```bash
dotnet run
```

Open http://localhost:5011/scalar to explore the API. Switch between v1 and v2 using the version dropdown.

## Example Requests

Use the included `.http` files to test:
- `querystring.http` — Query string versioning
- `header.http` — HTTP header versioning

### Query String
- Users v1: `GET http://localhost:5011/api/users?api-version=1.0`
- Users v2: `GET http://localhost:5011/api/users?api-version=2.0`
- Scores v1: `GET http://localhost:5011/api/scores?api-version=1.0`
- Scores v2: `GET http://localhost:5011/api/scores?api-version=2.0`

### Version-Neutral
- Delete User: `DELETE http://localhost:5011/api/users/{id}`

## OpenAPI Documents

- v1: `GET http://localhost:5011/openapi/v1.json`
- v2: `GET http://localhost:5011/openapi/v2.json`
