using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning();

var app = builder.Build();

var usersApi = app.NewVersionedApi("Users");

var usersv1 = usersApi.MapGroup("api/users").HasApiVersion(1.0);
var usersv2 = usersApi.MapGroup("api/users").HasApiVersion(2.0);
var usersNeutral = usersApi.MapGroup("api/users").IsApiVersionNeutral();

usersv1.MapGet("", () => TypedResults.Ok(new[]
{
    new UserV1(1, "John Doe"),
    new UserV1(2, "Alice Dewett"),
}));

usersv2.MapGet("", () => TypedResults.Ok(new[]
{
    new UserV2(1, "John Doe", new DateOnly(1990, 1, 1)),
    new UserV2(2, "Alice Dewett", new DateOnly(1992, 2, 2)),
}));

usersNeutral.MapDelete("{id:int}", (int id) =>
{
    // Delete user logic here
    return TypedResults.NoContent();
}).IsApiVersionNeutral();

app.Run();

record UserV1(int Id, string Name);
record UserV2(int Id, string Name, DateOnly BirthDate);
