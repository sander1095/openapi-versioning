var builder = WebApplication.CreateBuilder(args).AddServiceDefaults();

builder.Services.AddControllers();

builder.Services.AddApiVersioning()
    .AddMvc();

var app = builder.Build();

app.MapControllers();
app.Run();
