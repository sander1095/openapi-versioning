using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

string[] versions = ["v1", "v2"];
foreach (var description in versions)
{
    builder.Services.AddOpenApi(description, options =>
    {
        // These OpenAPI transformers change the document to be more compatible with API versioning.
        // The original source for these transformers, and other useful ones, can be found at
        // https://github.com/dotnet/eShop/blob/5624ad564d1602a927879df32a79b94522eb6101/src/eShop.ServiceDefaults/OpenApiOptionsExtensions.cs
        options.ApplyApiVersionInfo("Title", "Description");
    });
}

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;

    // API versioning by URL segment (api/v1/users)
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";

    // SubstituteApiVersionInUrl replaces the {version:apiVersion} placeholder with the actual
    // version number. Swagger UI then shows /api/v1/... or /api/v2/... depending on the selected version.
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
app.UseSwaggerUI(options =>
{
    // We reverse the list of api versions so the newest version is rendered first
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

internal static class OpenApiOptionsExtensions
{
    public static OpenApiOptions ApplyApiVersionInfo(this OpenApiOptions options, string title, string description)
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            var versionedDescriptionProvider = context.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            var apiDescription = versionedDescriptionProvider?.ApiVersionDescriptions
                .SingleOrDefault(description => description.GroupName == context.DocumentName);
            if (apiDescription is null)
            {
                return Task.CompletedTask;
            }
            document.Info.Version = apiDescription.ApiVersion.ToString();
            document.Info.Title = title;
            document.Info.Description = BuildDescription(apiDescription, description);
            return Task.CompletedTask;
        });
        return options;
    }

    private static string BuildDescription(ApiVersionDescription api, string description)
    {
        var text = new StringBuilder(description);

        if (api.IsDeprecated)
        {
            if (text.Length > 0)
            {
                if (text[^1] != '.')
                {
                    text.Append('.');
                }

                text.Append(' ');
            }

            text.Append("This API version has been deprecated.");
        }

        if (api.SunsetPolicy is { } policy)
        {
            if (policy.Date is { } when)
            {
                if (text.Length > 0)
                {
                    text.Append(' ');
                }

                text.Append("The API will be sunset on ")
                    .Append(when.Date.ToShortDateString())
                    .Append('.');
            }

            if (policy.HasLinks)
            {
                text.AppendLine();

                var rendered = false;

                foreach (var link in policy.Links.Where(l => l.Type == "text/html"))
                {
                    if (!rendered)
                    {
                        text.Append("<h4>Links</h4><ul>");
                        rendered = true;
                    }

                    text.Append("<li><a href=\"");
                    text.Append(link.LinkTarget.OriginalString);
                    text.Append("\">");
                    text.Append(
                        StringSegment.IsNullOrEmpty(link.Title)
                        ? link.LinkTarget.OriginalString
                        : link.Title.ToString());
                    text.Append("</a></li>");
                }

                if (rendered)
                {
                    text.Append("</ul>");
                }
            }
        }

        return text.ToString();
    }
}
