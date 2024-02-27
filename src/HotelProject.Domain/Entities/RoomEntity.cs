namespace HotelProject.Domain.Entities;

public class RoomEntity
{
    public Guid Id { get; set; } = new();
    public Guid TypeId { get; set; }

    public string Name { get; set; } = string.Empty;
    public int FloorLevel { get; set; }

    public bool IsCleaned { get; set; }
    public bool IsActive { get; set; }

    public RoomTypeEntity Type { get; set; } = null!;
    public List<BookingEntity> Bookings { get; set; } = [];
}
