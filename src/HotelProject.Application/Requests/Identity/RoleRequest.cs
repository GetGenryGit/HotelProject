using System.ComponentModel.DataAnnotations;

namespace HotelProject.Application.Requests.Identity;

public class RoleRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
