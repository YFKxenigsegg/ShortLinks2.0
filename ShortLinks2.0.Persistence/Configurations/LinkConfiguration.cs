using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortLinks.Domain.Entities;

namespace ShortLinks.Persistence.Configurations;
public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<User>(x => x.UserId)
            .IsRequired();
    }
}
