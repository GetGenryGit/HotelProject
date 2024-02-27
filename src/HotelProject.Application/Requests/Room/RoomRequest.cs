using System.ComponentModel.DataAnnotations;

namespace HotelProject.Application.Requests.Room;

public class RoomRequest
{
    [Required]
    public Guid TypeId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public int FloorLevel { get; set; }
    public bool IsCleaned { get; set; }
    public bool IsActive { get; set; }
}
