var builder = DistributedApplication.CreateBuilder(args);

// v10 controllers
builder.AddProject("v10-controllers-minimal-setup-no-openapi", @"..\..\v10\controllers\minimal-setup-no-openapi\minimal-setup-no-openapi.csproj");
builder.AddProject("v10-controllers-queryheader-versioning-openapi", @"..\..\v10\controllers\queryheader-versioning-openapi\queryheader-versioning-openapi.csproj");
builder.AddProject("v10-controllers-queryheader-versioning-openapi-scalar", @"..\..\v10\controllers\queryheader-versioning-openapi-scalar\queryheader-versioning-openapi-scalar.csproj");
builder.AddProject("v10-ctrl-queryheader-versioning-openapi-swaggerui", @"..\..\v10\controllers\queryheader-versioning-openapi-swaggerui\queryheader-versioning-openapi-swaggerui.csproj");
builder.AddProject("v10-controllers-url-versioning-openapi", @"..\..\v10\controllers\url-versioning-openapi\url-versioning-openapi.csproj");
builder.AddProject("v10-controllers-url-versioning-openapi-scalar", @"..\..\v10\controllers\url-versioning-openapi-scalar\url-versioning-openapi-scalar.csproj");
builder.AddProject("v10-controllers-url-versioning-openapi-swaggerui", @"..\..\v10\controllers\url-versioning-openapi-swaggerui\url-versioning-openapi-swaggerui.csproj");

// v10 minimal-api
builder.AddProject("v10-minimal-api-aot-versioning-openapi", @"..\..\v10\minimal-api\aot-versioning-openapi\aot-versioning-openapi.csproj");
builder.AddProject("v10-minimal-api-minimal-setup-no-openapi", @"..\..\v10\minimal-api\minimal-setup-no-openapi\minimal-setup-no-openapi.csproj");
builder.AddProject("v10-minimal-api-queryheader-versioning-openapi", @"..\..\v10\minimal-api\queryheader-versioning-openapi\queryheader-versioning-openapi.csproj");
builder.AddProject("v10-minimal-api-queryheader-versioning-openapi-scalar", @"..\..\v10\minimal-api\queryheader-versioning-openapi-scalar\queryheader-versioning-openapi-scalar.csproj");
builder.AddProject("v10-min-api-queryheader-versioning-openapi-swaggerui", @"..\..\v10\minimal-api\queryheader-versioning-openapi-swaggerui\queryheader-versioning-openapi-swaggerui.csproj");
builder.AddProject("v10-minimal-api-url-versioning-openapi", @"..\..\v10\minimal-api\url-versioning-openapi\url-versioning-openapi.csproj");
builder.AddProject("v10-minimal-api-url-versioning-openapi-scalar", @"..\..\v10\minimal-api\url-versioning-openapi-scalar\url-versioning-openapi-scalar.csproj");
builder.AddProject("v10-minimal-api-url-versioning-openapi-swaggerui", @"..\..\v10\minimal-api\url-versioning-openapi-swaggerui\url-versioning-openapi-swaggerui.csproj");

// v8 controllers
builder.AddProject("v8-controllers-minimal-setup-no-openapi", @"..\..\v8\controllers\minimal-setup-no-openapi\minimal-setup-no-openapi.csproj");
builder.AddProject("v8-controllers-queryheader-versioning-openapi", @"..\..\v8\controllers\queryheader-versioning-openapi\queryheader-versioning-openapi.csproj");
builder.AddProject("v8-controllers-queryheader-versioning-openapi-scalar", @"..\..\v8\controllers\queryheader-versioning-openapi-scalar\queryheader-versioning-openapi-scalar.csproj");
builder.AddProject("v8-ctrl-queryheader-versioning-openapi-swaggerui", @"..\..\v8\controllers\queryheader-versioning-openapi-swaggerui\queryheader-versioning-openapi-swaggerui.csproj");
builder.AddProject("v8-controllers-url-versioning-openapi", @"..\..\v8\controllers\url-versioning-openapi\url-versioning-openapi.csproj");
builder.AddProject("v8-controllers-url-versioning-openapi-scalar", @"..\..\v8\controllers\url-versioning-openapi-scalar\url-versioning-openapi-scalar.csproj");
builder.AddProject("v8-controllers-url-versioning-openapi-swaggerui", @"..\..\v8\controllers\url-versioning-openapi-swaggerui\url-versioning-openapi-swaggerui.csproj");

// v8 minimal-api
builder.AddProject("v8-minimal-api-aot-versioning-openapi", @"..\..\v8\minimal-api\aot-versioning-openapi\aot-versioning-openapi.csproj");
builder.AddProject("v8-minimal-api-minimal-setup-no-openapi", @"..\..\v8\minimal-api\minimal-setup-no-openapi\minimal-setup-no-openapi.csproj");
builder.AddProject("v8-minimal-api-queryheader-versioning-openapi", @"..\..\v8\minimal-api\queryheader-versioning-openapi\queryheader-versioning-openapi.csproj");
builder.AddProject("v8-minimal-api-queryheader-versioning-openapi-scalar", @"..\..\v8\minimal-api\queryheader-versioning-openapi-scalar\queryheader-versioning-openapi-scalar.csproj");
builder.AddProject("v8-min-api-queryheader-versioning-openapi-swaggerui", @"..\..\v8\minimal-api\queryheader-versioning-openapi-swaggerui\queryheader-versioning-openapi-swaggerui.csproj");
builder.AddProject("v8-minimal-api-url-versioning-openapi", @"..\..\v8\minimal-api\url-versioning-openapi\url-versioning-openapi.csproj");
builder.AddProject("v8-minimal-api-url-versioning-openapi-scalar", @"..\..\v8\minimal-api\url-versioning-openapi-scalar\url-versioning-openapi-scalar.csproj");
builder.AddProject("v8-minimal-api-url-versioning-openapi-swaggerui", @"..\..\v8\minimal-api\url-versioning-openapi-swaggerui\url-versioning-openapi-swaggerui.csproj");

builder.Build().Run();
