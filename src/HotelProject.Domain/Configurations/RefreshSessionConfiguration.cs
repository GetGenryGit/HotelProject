using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelProject.Domain.Configurations;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSessionEntity>
{
    public void Configure(EntityTypeBuilder<RefreshSessionEntity> builder)
    {
        builder.HasKey(s => s.Id);

        builder
            .HasOne(u => u.User)
            .WithMany(s => s.RefreshSessions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
