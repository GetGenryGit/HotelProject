using HotelProject.Domain.Entities;

namespace HotelProject.Application.Interfaces.Repositories.Identity;

public interface IRolesRepository
{
    Task<List<RoleEntity>> Get();
    Task<RoleEntity?> GetByName(string title);
    Task Add(string role, string description);
    Task Delete(Guid id);
    Task Update(Guid id, string title, string description);

}
