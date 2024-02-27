using HotelProject.Domain.Entities;

namespace HotelProject.Application.Interfaces.Repositories.Identity;

public interface IUsersRepository
{
    Task<List<UserEntity>> Get();
    Task<UserEntity?> GetById(Guid id);
    Task<UserEntity?> GetUserByUserName(string userName);
    Task<UserEntity?> GetUserByEmail(string email);
    Task<UserEntity?> GetUserByPhone(string phone);


    Task Add(Guid roleId, string firstName, string lastName, string? middleName,
        string userName, string password, string email, string phone, bool isActive = true);
    Task Update(Guid id, Guid roleId, string firstName, string lastName, string? middleName,
        string email, string userName, string password, string phone,
        bool isActive = true);

    Task Delete(Guid id);

}
