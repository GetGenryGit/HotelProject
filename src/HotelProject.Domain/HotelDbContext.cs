using HotelProject.Domain.Configurations;
using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Domain;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) 
    : DbContext(options)
{
    public DbSet<RefreshSessionEntity> RefreshSessions { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoomTypeEntity> RoomTypes { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<BookingStatusEntity> BookingStatuses { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RefreshSessionConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new BookingStatusConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        base.OnModelCreating(modelBuilder);
    }
}
