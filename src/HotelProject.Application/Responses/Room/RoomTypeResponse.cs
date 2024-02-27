namespace HotelProject.Application.Responses.Room;

public class RoomTypeResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PriceOneNight { get; set; }
    public int MaxPersons { get;set; }
}
