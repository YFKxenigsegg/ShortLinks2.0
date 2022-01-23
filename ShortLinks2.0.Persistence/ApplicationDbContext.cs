using Microsoft.EntityFrameworkCore;
using ShortLinks.Domain.Entities;
using ShortLinks.Kernel.Helpers;
using System.Reflection;

namespace ShortLinks.Persistence;
public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Link> Links { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyDbConverter, DateOnlyDbComparer>()
                .HaveColumnType("date");
    }
}
