using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Postgres.Data;

public class DbSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        await context.Database.EnsureCreatedAsync();
        await PopulateCountriesAsync();
        await PopulateTeamsAsync();
    }

    private async Task PopulateCountriesAsync()
    {
        if (context.Countries.Any()) return;
        
        string countriesSqlScript = await File.ReadAllTextAsync("Data/SqlScripts/Countries.sql");
        await context.Database.ExecuteSqlRawAsync(countriesSqlScript);
    }

    private async Task PopulateTeamsAsync()
    {
        if (context.Teams.Any()) return;

        foreach (var country in context.Countries)
        {
            context.Teams.Add(new Team {Name = country.Name, Country = country});           
            
            if (country.Name == "Spain")
            {
                context.Teams.Add(new Team {Name = "Real Madrid", Country = country});
                context.Teams.Add(new Team {Name = "Barcelona", Country = country});
                context.Teams.Add(new Team {Name = "Valencia", Country = country});
                context.Teams.Add(new Team {Name = "Atlético de Madrid", Country = country});
                context.Teams.Add(new Team {Name = "Levante", Country = country});
                context.Teams.Add(new Team {Name = "Betis", Country = country});
            }
            else if (country.Name == "England")
            {
                context.Teams.Add(new Team {Name = "Manchester United", Country = country});
                context.Teams.Add(new Team {Name = "Liverpool", Country = country});
            }
            else if (country.Name == "Germany")
            {
                context.Teams.Add(new Team {Name = "Bayern Munich", Country = country});
                context.Teams.Add(new Team {Name = "Borussia Dortmund", Country = country});
            }
            else if (country.Name == "Argentina")
            {
                context.Teams.Add(new Team {Name = "Boca Juniors", Country = country});
                context.Teams.Add(new Team {Name = "River Plate", Country = country});
            }
        }
        
        await context.SaveChangesAsync();
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