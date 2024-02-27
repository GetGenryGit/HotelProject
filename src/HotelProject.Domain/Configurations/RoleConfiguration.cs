using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProject.Domain.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasIndex(r => r.Name)
            .IsUnique();

        builder
            .HasMany(r => r.Users)
            .WithOne(u => u.Role);
    }
}
