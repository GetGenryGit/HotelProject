using HotelProject.Application.Responses.Identity;
using HotelProject.Domain.Entities;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Application.Interfaces.Services.Identity;

public interface IUserService
{
    Task<Result<List<UserResponse>>> GetAllAsync();

    Task<Result<UserResponse>> GetUserById(Guid id);

    Task<Result<RoleResponse>> GetUserRoleByUserId(Guid id);

    Task<Result> RegisterUser(string firstName, string lastName, string? middleName,
        string userName, string password, string email, string phone);
    Task<Result> UpdateUser(Guid id,
        string firstName, string lastName, string? middleName,
        string userName, string password, string email, string phone,
        string roleName, bool isActive = true);
}
