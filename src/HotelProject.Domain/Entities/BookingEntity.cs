namespace HotelProject.Domain.Entities;

public class BookingEntity
{
    public Guid Id { get; set; } = new();
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public Guid StatusId { get; set; }

    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
    public int PersonCount { get; set; }
    public decimal TotalPrice { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public RoomEntity Room { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public BookingStatusEntity Status { get; set; } = null!;

}
