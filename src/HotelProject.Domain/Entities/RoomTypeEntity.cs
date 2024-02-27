namespace HotelProject.Domain.Entities;

public class RoomTypeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MaxPersons { get; set; }
    public decimal PriceOneNight { get; set; }

    public List<RoomEntity> Rooms { get; set; } = [];
}
