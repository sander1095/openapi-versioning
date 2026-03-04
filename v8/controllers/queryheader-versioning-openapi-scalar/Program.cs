using System.Text;
using System.Text.Json.Nodes;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
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
        // These OpenAPI transformers change the document to be more compatible with API versioning.
        // The original source for these transformers, and other useful ones, can be found at
        // https://github.com/dotnet/eShop/blob/5624ad564d1602a927879df32a79b94522eb6101/src/eShop.ServiceDefaults/OpenApiOptionsExtensions.cs
        options.ApplyApiVersionInfo("Title", "Description");
        // ApplyApiVersionDescription sets the Example value on the api-version parameter schema.
        // Scalar reads this and pre-fills the correct version value in the UI automatically.
        options.ApplyApiVersionDescription();
    });
}

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
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
    options.GroupNameFormat = "'v'VVV";
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

app.MapControllers();

app.Run();

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

    /// <summary>
    /// Sets a description and version-specific example on the api-version parameter.
    /// Scalar reads the Example value and pre-fills the field automatically, giving users
    /// the correct version to use without having to guess.
    /// </summary>
    public static OpenApiOptions ApplyApiVersionDescription(this OpenApiOptions options)
    {
        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
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
