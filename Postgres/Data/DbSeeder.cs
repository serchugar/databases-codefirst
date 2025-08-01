using Microsoft.EntityFrameworkCore;

namespace Postgres.Data;

public class DbSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        await context.Database.EnsureCreatedAsync();
        await PopulateCountriesAsync();
    }

    private async Task PopulateCountriesAsync()
    {
        if (context.Countries.Any()) return;
        
        string countriesSqlScript = await File.ReadAllTextAsync("Data/SqlScripts/Countries.sql");
        await context.Database.ExecuteSqlRawAsync(countriesSqlScript);
    }
}

public static class DbSeederExtensions
{
    public static async Task<IApplicationBuilder> UseDbSeederAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        DbSeeder dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
        await dbSeeder.SeedAsync();

        return app;
    }
}