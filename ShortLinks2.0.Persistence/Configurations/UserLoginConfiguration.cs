using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortLinks.Domain.Entities;

namespace ShortLinks.Persistence.Configurations;
public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(96);

        builder.Property(x => x.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(96);

        builder.Property(x => x.CreateTime)
            .HasColumnName("CreateTime")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.LastLoginTime)
            .HasColumnName("LastLoginTime")
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(x => x.UserRoleId)
            .HasColumnName("UserRoleId")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(256);

        builder.HasOne(x => x.UserRole)
            .WithMany(x => x.UserLogin)
            .IsRequired()
            .HasForeignKey(x => x.UserRoleId);

        builder.Ignore(x => x.AccessFailedCount);
        builder.Ignore(x => x.ConcurrencyStamp);
        builder.Ignore(x => x.EmailConfirmed);
        builder.Ignore(x => x.LockoutEnabled);
        builder.Ignore(x => x.LockoutEnd);
        builder.Ignore(x => x.NormalizedEmail);
        builder.Ignore(x => x.NormalizedUserName);
        builder.Ignore(x => x.PhoneNumber);
        builder.Ignore(x => x.PhoneNumberConfirmed);
        builder.Ignore(x => x.SecurityStamp);
        builder.Ignore(x => x.TwoFactorEnabled);
        builder.Ignore(x => x.UserName);
    }
}
