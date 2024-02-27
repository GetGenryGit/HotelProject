using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProject.Domain.Configurations;

public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatusEntity>
{
    public void Configure(EntityTypeBuilder<BookingStatusEntity> builder)
    {
        builder
            .HasKey(bs => bs.Id);

        builder
            .HasIndex(bs => bs.Name)
            .IsUnique();

        builder
            .HasMany(bs => bs.Bookings)
            .WithOne(b => b.Status);
    }
}
