using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProject.Domain.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasIndex(r => r.Name)
            .IsUnique();

        builder
            .HasOne(r => r.Type)
            .WithMany(t => t.Rooms)
            .HasForeignKey(r => r.TypeId);

        builder
            .HasMany(r => r.Bookings)
            .WithOne(t => t.Room);
    }
}
