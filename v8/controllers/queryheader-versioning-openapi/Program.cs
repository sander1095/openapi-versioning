using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args).AddServiceDefaults();

builder.Services.AddOpenApi("v1");
builder.Services.AddOpenApi("v2");

builder.Services.AddControllers();

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
    options.AssumeDefaultVersionWhenUnspecified = true;

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

    // GroupNameFormat specifies the format of the API version.
    // Without this, versioning will use the literal group names. In our case, that would be 1.0.
    // For compatibility with the "default" /openapi/v1.json behavior from Microsoft.AspNetCore.OpenApi, we use v'VVV' so we can retrieve it using v1.json, v1.0.json and more.
    // See https://github.com/dotnet/aspnet-api-versioning/wiki/Version-Format#custom-api-version-format-strings for more information about formatting API versions.
    options.GroupNameFormat = "'v'VVV";
});

var app = builder.Build();

app.MapOpenApi();

app.MapControllers();

app.Run();
