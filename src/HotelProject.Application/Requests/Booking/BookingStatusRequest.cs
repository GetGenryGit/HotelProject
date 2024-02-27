using System.ComponentModel.DataAnnotations;

namespace HotelProject.Application.Requests.Booking;

public class BookingStatusRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
}
