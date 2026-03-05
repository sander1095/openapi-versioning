using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    // Supported/deprecated API versions will be reported in response headers
    // This is optional, but can be useful for clients to understand what versions are available
    // or for logging and analytics purposes
    options.ReportApiVersions = true;

    // API versioning by URL segment (api/v1/users)
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    // Calling "AddApiExplorer" is required for OpenAPI versioning to work correctly. 
    // Without this, the generated OpenAPI documents will not be versioned.

    // Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    // More information: https://github.com/dotnet/aspnet-api-versioning/wiki/Version-Format#custom-api-version-format-strings
    // Without this, the OpenAPI document will not generate correctly.
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
})
// You must call "AddOpenApi" after "AddApiVersioning" to ensure you use Asp.Versioning's variant.
// This variant of "AddOpenApi" is required to properly integrate with API versioning and generate versioned OpenAPI documents.
// Compared to ASP.Versioning v8, we no longer need to use custom transformers to modify the generated OpenAPI document for better versioning support.
.AddOpenApi();

var app = builder.Build();

// WithDocumentPerVersion() is an extension method provided by the Asp.Versioning.OpenApi package.
// It configures the OpenAPI endpoint to generate a separate document for each API version.
// This allows clients to retrieve documentation specific to the version of the API they are using.
// This approach is preferable compared to having to call "services.AddOpenApi()" multiple times for each version, which can lead to maintenance issues and potential misconfigurations when adding new versions.
app.MapOpenApi().WithDocumentPerVersion();

var usersApi = app.NewVersionedApi("Users");
var scoresApi = app.NewVersionedApi("Scores");

// --------- Users API ---------
var usersv1 = usersApi.MapGroup("api/v{version:apiVersion}/users")
    .HasApiVersion(1.0);

var usersv2 = usersApi.MapGroup("api/v{version:apiVersion}/users")
    .HasApiVersion(2.0);

var usersNeutral = usersApi.MapGroup("api/users")
    .IsApiVersionNeutral();

usersv1.MapGet("", () =>
{
    return TypedResults.Ok(new[]
    {
        new UserV1(1, "John Doe", "johndoe@example.com"),
        new UserV1(2, "Alice Dewett", "alice@example.com"),
    });
});

usersv2.MapGet("", () =>
{
    return TypedResults.Ok(new[]
    {
        new UserV2(1, "John Doe", "johndoe@example.com", new DateOnly(1990, 1, 1)),
        new UserV2(2, "Alice Dewett", "alice@example.com", new DateOnly(1992, 2, 2)),
    });
});

usersNeutral.MapDelete("{id:int}", (int id) =>
{
    // Delete user logic here
    return TypedResults.NoContent();
})
.IsApiVersionNeutral();


// --------- Scores API ---------
var scoresv1 = scoresApi.MapGroup("api/v{version:apiVersion}/scores")
    .HasApiVersion(1.0);

var scoresv2 = scoresApi.MapGroup("api/v{version:apiVersion}/scores")
    .HasApiVersion(2.0);

scoresv1.MapGet("", () =>
{
    return TypedResults.Ok(new ScoreV1[]
    {
        new(1, 100),
        new(2, 150)
    });
});

scoresv2.MapGet("", () =>
{
    return TypedResults.Ok(new ScoreV2[]
    {
        new(1, 100, DateTimeOffset.UtcNow.AddDays(-2)),
        new(2, 150, DateTimeOffset.UtcNow.AddDays(-1))
    });
});

// UseSwaggerUI MUST come after MapOpenApi() and the API endpoint definitions,
// possibly due to the use of DescribeApiVersions()
// Because SubstituteApiVersionInUrl = true, selecting V1 shows /api/v1/... paths
// and selecting V2 shows /api/v2/... paths — the version is already in the URL.
app.UseSwaggerUI(options =>
{
    // We reverse the list api versions so the newest version is rendered first
    foreach (var description in app.DescribeApiVersions().Reverse())
    {
        options.SwaggerEndpoint(
            $"/openapi/{description.GroupName}.json",
            description.GroupName.ToUpperInvariant());
    }
});

app.Run();

record UserV1(int Id, string Name, string Email);
record UserV2(int Id, string Name, string Email, DateOnly BirthDate);

record ScoreV1(int UserId, int Score);
record ScoreV2(int UserId, int Score, DateTimeOffset AchievedOn);
