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

    // Set the default API version to 1.0 explicitly
    // This is already set to 1.0 by default, but shown here for demonstration 
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // If the user does not specify a version, you can let the API use the default version
    // This is disabled by default.
    // Enabling this feature is a trade-off between convenience and explicitness.
    // Changing the default version could break clients that aren't using versioning.
    // Consider your API's audience and usage patterns when deciding to enable this.
    options.AssumeDefaultVersionWhenUnspecified = false;

    // API versioning by query string (default approach)
    // Using query string: ?api-version=1.0
    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");

    // Alternative: API versioning by HTTP header
    // Uncomment the line below to use header-based versioning instead
    // options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

    // You can also combine multiple readers
    // options.ApiVersionReader = ApiVersionReader.Combine(
    //     new QueryStringApiVersionReader("api-version"),
    //     new HeaderApiVersionReader("x-api-version")
    // );
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
});

var app = builder.Build();

app.MapOpenApi();

var usersApi = app.NewVersionedApi("Users");
var scoresApi = app.NewVersionedApi("Scores");

// --------- Users API ---------
// Note: No version placeholder in URL path
var usersv1 = usersApi.MapGroup("api/users")
    .HasApiVersion(1.0);

var usersv2 = usersApi.MapGroup("api/users")
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
var scoresv1 = scoresApi.MapGroup("api/scores")
    .HasApiVersion(1.0);

var scoresv2 = scoresApi.MapGroup("api/scores")
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
