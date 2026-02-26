var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioning()
    .AddMvc();

var app = builder.Build();

app.MapControllers();
app.Run();
