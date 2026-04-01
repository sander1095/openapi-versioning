using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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

    // GroupNameFormat specifies the format of the API version.
    // Without this, versioning will use the literal group names. In our case, that would be 1.0.
    // For compatibility with the "default" /openapi/v1.json behavior from Microsoft.AspNetCore.OpenApi, we use v'VVV' so we can retrieve it using v1.json, v1.0.json and more.
    // See https://github.com/dotnet/aspnet-api-versioning/wiki/Version-Format#custom-api-version-format-strings for more information about formatting API versions.
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
})
.AddMvc()
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

app.MapControllers();

// UseSwaggerUI MUST come after MapOpenApi() and the API endpoint definitions,
// possibly due to the use of DescribeApiVersions()
// Because SubstituteApiVersionInUrl = true, selecting V1 shows /api/v1/... paths
// and selecting V2 shows /api/v2/... paths — the version is already in the URL.
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
