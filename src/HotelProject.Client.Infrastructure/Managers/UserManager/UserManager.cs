using Client.Infrastructure.Extensions;
using Client.Infrastructure.Routes.Identity;
using Client.Infrastructure.Services;
using HotelProject.Application.Requests.Identity;
using HotelProject.Application.Responses.Identity;
using HotelProject.Shared.Wrapper;
using System.Net.Http.Json;

namespace Client.Infrastructure.Managers.UserManager;

public class UserManager(IHttpService httpService) 
    : IUserManager
{
    private readonly IHttpService _httpService = httpService;
    public async Task<IResult<List<UserResponse>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IResult<UserResponse>> GetAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> RegisterUserAsync(RegisterRequest model)
    {
        var response = await _httpService.Post(UserEndpoints.Register, model);
        return await response.ToResult();
    }
}
