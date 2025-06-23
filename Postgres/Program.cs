using Microsoft.EntityFrameworkCore;
using Postgres.Configuration;
using Postgres.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();
#region Services
builder.Services.AddDbContextPool<AppDbContext>(opts => 
    opts.UseNpgsql(
        builder.Configuration.GetConnectionString("testing"),
        o => o.MigrationsHistoryTable("__EFMigrationHistory", "code_first")));
builder.Services.AddDependencyInjectionConfig();
builder.Services.AddControllers();
if(builder.Environment.IsDevelopment()) builder.Services.AddSwaggerConfig();
#endregion

WebApplication app = builder.Build();
#region Middleware
if(app.Environment.IsDevelopment()) app.UseSwaggerConfig();
#endregion

app.MapControllers();
app.Run();