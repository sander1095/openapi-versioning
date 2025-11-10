using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddOpenApi("v1");
builder.Services.AddOpenApi("v2");

builder.Services.AddApiVersioning(x =>
{
    x.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";
});

var app = builder.Build();

app.MapOpenApi();

var usersApi = app.NewVersionedApi("Users");
var scoresApi = app.NewVersionedApi("Scores");


// --------- Users API ---------
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

[JsonSerializable(typeof(Userv1[]))]
[JsonSerializable(typeof(Userv2[]))]
[JsonSerializable(typeof(Scorev1[]))]
[JsonSerializable(typeof(Scorev2[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
