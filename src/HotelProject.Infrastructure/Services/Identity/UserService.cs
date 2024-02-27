using HotelProject.Application.Interfaces.Repositories.Identity;
using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Application.Responses.Identity;
using HotelProject.Infrastructure.Helpers;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Infrastructure.Services.Identity;

public class UserService(IUsersRepository usersRepository,
    IRolesRepository rolesRepository, IPasswordHasher passwordHasher) 
    : IUserService
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IRolesRepository _rolesRepository = rolesRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<List<UserResponse>>> GetAllAsync()
    {
        var users = await _usersRepository.Get();

        var usersResponse = new List<UserResponse>();

        foreach (var u in users)
        {
            usersResponse.Add(
            new UserResponse
            {
                Id = u.Id,

                FirstName = u.FirstName,
                LastName = u.LastName,
                MiddleName = u.MiddleName,

                UserName = u.UserName,

                Email = u.Email,
                Phone = u.Phone,

                IsActive = u.IsActive,
            });
        }

        return await Result<List<UserResponse>>.SuccessAsync(usersResponse);
    }

    public async Task<Result<UserResponse>> GetUserById(Guid id)
    {
        var user = await _usersRepository.GetById(id);
        if (user == null)
        {
            return await Result<UserResponse>
                .FailAsync("Пользователь с данным айди не найден!");
        }

        var userResponse = new UserResponse
        {
            Id = user.Id,

            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,

            UserName = user.UserName,

            Email = user.Email,
            Phone = user.Phone,

            IsActive = user.IsActive,
        };

        return await Result<UserResponse>.SuccessAsync(userResponse);
    }

    public async Task<Result<RoleResponse>> GetUserRoleByUserId(Guid id)
    {
        var user = await _usersRepository.GetById(id);
        if (user == null)
        {
            return await Result<RoleResponse>
                .FailAsync("Пользователь с данным айди не найден!");
        }

        var roleResponse = new RoleResponse
        {
            Id = user.Role.Id,

            Name = user.Role.Name,
            Description = user.Role.Description,
        };

        return await Result<RoleResponse>
            .SuccessAsync(roleResponse);
    }

    public async Task<Result> RegisterUser(string firstName, string lastName, string? middleName,
        string userName, string password, string email, string phone)
    {
        var userWithSameEmail = await _usersRepository.GetUserByEmail(email);
        if (userWithSameEmail != null)
        {
            return await Result<string>
                .FailAsync("Пользователь с такой почтой уже существует!");
        }
        var userWithSameUserName = await _usersRepository.GetUserByUserName(userName);
        if (userWithSameUserName != null)
        {
            return await Result<string>
                .FailAsync("Пользователь с таким именем уже существует!");
        }

        var userWithSamePhone = await _usersRepository.GetUserByPhone(phone);
        if (userWithSamePhone != null)
        {
            return await Result<string>
                .FailAsync("Пользователь с таким номером телефона уже существует!");
        }

        var newUserRoleEntity = await _rolesRepository.GetByName("Гость");
        if (newUserRoleEntity == null) 
        {
            return await Result<string>
                .FailAsync("Роли гость не существует!");
        }

        var hashedPassword = _passwordHasher.Generate(password);
        await _usersRepository.Add(newUserRoleEntity.Id,
            firstName, lastName, middleName,
            userName, hashedPassword, email, phone);
        return await Result<string>
            .SuccessAsync("Пользователь успешно прошёл регистрацию!");
    }

    public async Task<Result> UpdateUser(Guid id,
        string firstName, string lastName, string? middleName,
        string userName, string password, string email, string phone,
        string roleName, bool isActive = true)
    {
        var userRoleEntity = await _rolesRepository.GetByName(roleName);
        if (userRoleEntity == null) 
        {
            return await Result<string>
                .FailAsync($"Роли {roleName} не существует!");
        }

        var passwordHash = _passwordHasher.Generate(password);

        await _usersRepository.Update(id, userRoleEntity.Id, firstName, lastName, middleName,
            userName, passwordHash, email, phone, isActive);
        return await Result<string>.SuccessAsync("Пользователь успешно обновлен!");
    }
}
