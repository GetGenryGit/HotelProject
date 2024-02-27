using HotelProject.Application.Interfaces.Repositories.Identity;
using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Application.Responses.Identity;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Infrastructure.Services.Identity;

public class RoleService(IRolesRepository rolesRepository)
    : IRoleService
{
    private readonly IRolesRepository _rolesRepository = rolesRepository;

    public async Task<Result<List<RoleResponse>>> Get()
    {
        var roles = await _rolesRepository.Get();

        var rolesResponse = new List<RoleResponse>();

        foreach (var r in roles)
        {
            rolesResponse.Add(
                new()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                });
        }

        return await Result<List<RoleResponse>>
            .SuccessAsync(rolesResponse);
    }

    public async Task<Result> Add(string name, string description)
    {
        var roleWithSameTitle = await _rolesRepository.GetByName(name);
        if (roleWithSameTitle != null)
        {
            return await Result<string>
                .FailAsync("Роль с таким название уже существует!");
        }

        await _rolesRepository.Add(name, description);
        var result = await Result<string>
            .SuccessAsync("Новая роль успешно добавлена!");
        return result;
    }

    public async Task<Result> Delete(Guid id)
    {
        await _rolesRepository.Delete(id);
        return await Result<string>
            .SuccessAsync("Роль успешно удаленна!");
    }

    public async Task<Result> Update(Guid id, string name, string description)
    {
        await _rolesRepository.Update(id, name, description);
        return await Result<string>
            .SuccessAsync("Роль успешно изменена!");
    }
}
