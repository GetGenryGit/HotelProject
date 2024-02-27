namespace HotelProject.Application.Responses.Booking;

public class BookingResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }

    public string RoomName { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public int PersonCount { get; set; }

    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime Created { get; set; }
}
