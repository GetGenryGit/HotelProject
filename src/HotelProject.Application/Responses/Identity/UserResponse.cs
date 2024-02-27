namespace HotelProject.Application.Responses.Identity;

public class UserResponse
{
    public Guid Id { get; set;}

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public bool IsActive { get; set; } 

}
