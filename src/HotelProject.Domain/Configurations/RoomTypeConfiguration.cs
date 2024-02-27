using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProject.Domain.Configurations;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomTypeEntity>
{
    public void Configure(EntityTypeBuilder<RoomTypeEntity> builder)
    {
        builder
            .HasKey(rt => rt.Id);

        builder
            .HasIndex(rt => rt.Name)
            .IsUnique();

        builder
            .HasMany(rt => rt.Rooms)
            .WithOne(r => r.Type);
    }
}

