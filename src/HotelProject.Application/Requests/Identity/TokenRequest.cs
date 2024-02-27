using System.ComponentModel.DataAnnotations;

namespace HotelProject.Application.Requests.Identity;

public class TokenRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
