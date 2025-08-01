using Postgres.Data;

namespace Postgres.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
    {
        services.AddTransient<DbSeeder>();
        return services;
    }
}