using HotelProject.Application.Responses.Identity;
using HotelProject.Domain.Entities;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Application.Interfaces.Services.Identity;

public interface IRoleService
{
    Task<Result<List<RoleResponse>>> Get();
    Task<Result> Add(string title, string description);
    Task<Result> Update(Guid id, string title, string description);
    Task<Result> Delete(Guid id);
}
