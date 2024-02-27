namespace HotelProject.Application.Responses.Room;

public class RoomResponse
{
    public Guid Id { get; set; }

    public string TypeName { get; set; } = string.Empty;
    public string TypeDescription { get; set; } = string.Empty;
    public decimal PriceOneNight { get; set; }

    public string Name { get; set; } = string.Empty;
    public int FloorLevel { get; set; }

    public bool IsCleaned { get; set; }
    public bool IsActive { get; set; }
    public bool IsRoomNotBusy { get; set; }
}
