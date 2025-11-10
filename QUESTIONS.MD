# Questions About ASP.NET API Versioning (Asp.Versioning)

## OpenAPI Integration
- Do we need to manually call `AddOpenApi` for each version, or can Asp.Versioning handle this automatically?
  - Assumption: Probably not possible, since Asp.Versioning has no awareness of the OpenAPI package.

## Version Assignment Semantics
- What’s the difference between `HasApiVersion` and `MapToApiVersion`?
- Why does the v1 `users` endpoint return supported versions only for v1, while the v2 `users` endpoint reports supported versions for both v1 and v2?

## API Version Names
- What are API version names?
- Are version names useful in Minimal API applications?

## Group Names
- What are API version group names?
- How do group names differ from version names?

## API Version Sets
- What does it mean to group API versions into an API Version Set?
- When should version sets be used?

## API Version Conventions
- What are API Version Conventions?
- How do conventions interact with Minimal APIs?

## Minimal API Versioning Model
- Why can’t we simply create multiple `IEndpointRouteBuilder` instances (one per version) to define version-specific endpoints?
- What is the purpose of `IVersionedEndpointRouteBuilder`, and why doesn’t the straightforward approach using `app.NewVersionedApi()` work as expected?
- How should an `IEndpointRouteBuilder` be associated with multiple versions in practice?
