using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProject.Domain.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => u.UserName)
            .IsUnique();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder
            .HasIndex(u => u.Phone)
            .IsUnique();

        builder
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        builder
            .HasMany(s => s.RefreshSessions)
            .WithOne(u => u.User);
    }
}

