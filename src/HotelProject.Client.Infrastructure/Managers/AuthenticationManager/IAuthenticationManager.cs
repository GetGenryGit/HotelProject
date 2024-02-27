using HotelProject.Application.Requests.Identity;
using HotelProject.Shared.Wrapper;
using System.Security.Claims;

namespace Client.Infrastructure.Managers.AuthenticationManager;

public interface IAuthenticationManager
{
    Task<ClaimsPrincipal> CurrentUser();
    Task<IResult> Login(TokenRequest model);
    Task<IResult> Logout();
}
