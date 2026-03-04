using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Primitives;
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
    });
}

builder.Services.AddControllers();

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
    // version number in the generated OpenAPI document paths.
    // This means Scalar shows /api/v1/users and /api/v2/users — no manual input needed.
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

app.MapOpenApi();

// MapScalarApiReference sets up the Scalar UI at /scalar
// AddDocuments registers all known API versions so Scalar shows a dropdown to switch between them.
// Because SubstituteApiVersionInUrl = true, selecting v1 shows /api/v1/... paths
// and selecting v2 shows /api/v2/... paths — the version is already in the URL.
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
}
