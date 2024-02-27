namespace HotelProject.Application.Requests.Identity;

public class RefreshTokenRequest
{
    public string AuthToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
