using System.ComponentModel.DataAnnotations;

namespace HotelProject.Application.Requests.Room;

public class RoomTypeRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int MaxPersons { get; set; }
    [Required]
    public decimal PriceOneNight { get; set; }
}
