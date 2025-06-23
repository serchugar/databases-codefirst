using Postgres.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
#region Services
builder.Services.AddDependencyInjectionConfig();
builder.Services.AddControllers();
builder.Services.AddSwaggerConfig();
#endregion

WebApplication app = builder.Build();
#region Middleware
app.UseSwaggerConfig();
#endregion

app.MapControllers();
app.Run();