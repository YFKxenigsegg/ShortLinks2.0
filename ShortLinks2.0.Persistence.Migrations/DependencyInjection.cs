using Microsoft.Extensions.Configuration;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace ShortLinks.Persistence.Migrations;
public static class DependencyInjection
{
    public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(config =>
                config.AddSqlServer()
                .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
                .ScanIn()

            );


        return services;
    }
}
