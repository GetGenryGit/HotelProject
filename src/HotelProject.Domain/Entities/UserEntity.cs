namespace HotelProject.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; } = new();
    public Guid RoleId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; }

    public RoleEntity Role { get; set; } = null!;
    public List<RefreshSessionEntity> RefreshSessions { get; set; } = [];
    public List<BookingEntity> Bookings { get; set; } = [];

}
