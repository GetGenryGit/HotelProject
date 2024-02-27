using HotelProject.Application.Responses.Identity;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Application.Interfaces.Services.Identity;

public interface ITokenService
{
    Task<Result<TokenResponse>> Login(string username, string password);
    Task<Result<TokenResponse>> RefreshToken(string refreshToken);
    Task<Result<string>> RevokeToken(string refreshToken);

}
