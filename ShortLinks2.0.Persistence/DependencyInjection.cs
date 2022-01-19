using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShortLinks.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //inmem db?

        services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery))
            .ConfigureWarnings(warnings => warnings.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS))
                .EnableSensitiveDataLogging(true)
        );

        services.AddScoped(options =>
            options.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
                .CreateDbContext());


        //?
        var sp = services.BuildServiceProvider();

        using var scope = sp.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();

        using var db = dbContext.CreateDbContext();

        //Ensure
        db.Database.EnsureCreated();


        return services;
    }
}
