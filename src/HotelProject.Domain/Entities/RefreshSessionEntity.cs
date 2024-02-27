namespace HotelProject.Domain.Entities;

public class RefreshSessionEntity
{
    public Guid Id { get; set; } = new();
    public Guid UserId { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Expired { get; set; } = DateTime.UtcNow.AddDays(7);

    public UserEntity? User { get; set; }
}
