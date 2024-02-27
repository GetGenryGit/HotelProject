using HotelProject.Application.Requests.Identity;
using HotelProject.Application.Responses.Identity;
using HotelProject.Shared.Wrapper;

namespace Client.Infrastructure.Managers.UserManager;

public interface IUserManager
{
    Task<IResult<List<UserResponse>>> GetAllAsync();

    Task<IResult<UserResponse>> GetAsync(string userId);

    Task<IResult> RegisterUserAsync(RegisterRequest request);
}
