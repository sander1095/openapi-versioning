using System.Text;
using System.Text.Json.Nodes;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string[] versions = ["v1", "v2"];
foreach (var description in versions)
{
    builder.Services.AddOpenApi(description, options =>
    {
        options.ApplyApiVersionInfo("Title", "Description");
        options.ApplyApiVersionDescription();
    });
}

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

    // NOTE: For URL versioning, you will need SubstituteApiVersionInUrl
});

var app = builder.Build();

app.MapOpenApi();

// MapScalarApiReference sets up the Scalar UI at /scalar
// AddDocuments registers all known API versions so Scalar shows a dropdown to switch between them.
app.MapScalarApiReference(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    options.AddDocuments(provider.ApiVersionDescriptions.Select(d => d.GroupName));
});

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
var scoresv1 = scoresApi.MapGroup("api/scores")
    .HasApiVersion(1.0);

var scoresv2 = scoresApi.MapGroup("api/scores")
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

    public static OpenApiOptions ApplyAuthorizationChecks(this OpenApiOptions options, string[] scopes)
    {
        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
            var metadata = context.Description.ActionDescriptor.EndpointMetadata;

            if (!metadata.OfType<IAuthorizeData>().Any())
            {
                return Task.CompletedTask;
            }

            operation.Responses ??= new OpenApiResponses();
            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            var oAuthScheme = new OpenApiSecuritySchemeReference("oauth2", null);

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [oAuthScheme] = scopes.ToList()
                }
            };

            return Task.CompletedTask;
        });
        return options;
    }

    public static OpenApiOptions ApplyOperationDeprecatedStatus(this OpenApiOptions options)
    {
        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
            var apiDescription = context.Description;
            operation.Deprecated |= apiDescription.IsDeprecated();
            return Task.CompletedTask;
        });
        return options;
    }

    public static OpenApiOptions ApplyApiVersionDescription(this OpenApiOptions options)
    {
        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
            // Find parameter named "api-version" and add a description to it
            var apiVersionParameter = operation.Parameters?.FirstOrDefault(p => p.Name == "api-version");
            if (apiVersionParameter is not null)
            {
                apiVersionParameter.Description = "The API version, in the format 'major.minor'.";
                if (apiVersionParameter.Schema is OpenApiSchema targetSchema)
                {
                    switch (context.DocumentName)
                    {
                        case "v1":
                            targetSchema.Example = JsonNode.Parse("\"1.0\"");
                            break;
                        case "v2":
                            targetSchema.Example = JsonNode.Parse("\"2.0\"");
                            break;
                    }
                }
            }
            return Task.CompletedTask;
        });
        return options;
    }
}