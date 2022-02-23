using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace ShortLinks.Persistence.Migrations;
public static class DependencyInjection
{
    public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(config => config.AddSqlServer()
                .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
                .ScanIn().For.Migrations().For.EmbeddedResources())
            .AddLogging(lb => lb.AddFluentMigratorConsole().AddNLog());

        //initialize all the dependencies for SQL Server with RepoDb

        return services;
    }
}
