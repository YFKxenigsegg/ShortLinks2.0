using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortLinks.Domain.Entities;

namespace ShortLinks.Persistence.Configurations;
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Code)
            .HasColumnName("Code")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(24);

        builder.Ignore(x => x.ConcurrencyStamp);
        builder.Ignore(x => x.NormalizedName);
    }
}
