# Questions About ASP.NET API Versioning (Asp.Versioning)


## Can't use WithDocumentPerVersion()
- I'm trying to migrate the `v10` folder to the new way of working based on [the docs in the Release](https://github.com/dotnet/aspnet-api-versioning/releases/tag/v10.0.0-preview.1), but `WithDocumentPerVersion()` just doesn't want to compile.
   - Even with multiple restarts and cleans, it doesn't want to work. Is this an issue on my end?

## Breaking changes between .NET 8 and 10
- Is it intentional that the same code between v8 and v10 cause a breaking change? 
- What exactly CAUSES the breaking change (Handy to know for learning purposes)
  - No OpenAPI endpoints are returned in V10, though they are returned in V8
  - See commit 15d66973fe2e7a39e702a4bd96ba8abf17a4f962

## OpenAPI Integration
- Do we need to manually call `AddOpenApi` for each version, or can Asp.Versioning handle this automatically?
  - Assumption: Probably not possible, since Asp.Versioning has no awareness of the OpenAPI package.
  - ANSWER: V10 allows this with `WithDocumentPerVersion()` and a single `AddOpenApi()` call.

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
