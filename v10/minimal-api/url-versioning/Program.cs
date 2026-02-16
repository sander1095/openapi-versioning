using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1");
builder.Services.AddOpenApi("v2");

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
});

var app = builder.Build();

app.MapOpenApi();

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
        new Userv1(1, "John Doe", "johndoe@example.com"),
        new Userv1(2, "Alice Dewett", "alice@example.com"),
    });
});

usersv2.MapGet("", () =>
{
    return TypedResults.Ok(new[]
    {
        new Userv2(1, "John Doe", "johndoe@example.com", new DateOnly(1990, 1, 1)),
        new Userv2(2, "Alice Dewett", "alice@example.com", new DateOnly(1992, 2, 2)),
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
    return TypedResults.Ok(new Scorev1[]
    {
        new(1, 100),
        new(2, 150)
    });
});

scoresv2.MapGet("", () =>
{
    return TypedResults.Ok(new Scorev2[]
    {
        new(1, 100, DateTimeOffset.UtcNow.AddDays(-2)),
        new(2, 150, DateTimeOffset.UtcNow.AddDays(-1))
    });
});


app.Run();

record Userv1(int Id, string Name, string Email);
record Userv2(int Id, string Name, string Email, DateOnly BirthDate);

record Scorev1(int UserId, int Score);
record Scorev2(int UserId, int Score, DateTimeOffset AchievedOn);
