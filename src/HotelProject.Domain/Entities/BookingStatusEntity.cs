namespace HotelProject.Domain.Entities;

public class BookingStatusEntity
{
    public Guid Id { get; set; } = new();

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<BookingEntity> Bookings { get; set; } = [];
}
