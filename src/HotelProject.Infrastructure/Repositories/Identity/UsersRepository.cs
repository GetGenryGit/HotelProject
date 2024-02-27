using HotelProject.Application.Interfaces.Repositories.Identity;
using HotelProject.Domain;
using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Infrastructure.Repositories.Identity;

public class UsersRepository(HotelDbContext dbContext)
    : IUsersRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<List<UserEntity>> Get()
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .OrderByDescending(u => u.Created)
            .ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<UserEntity?> GetUserByUserName(string userName)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<UserEntity?> GetUserByEmail(string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserEntity?> GetUserByPhone(string phone)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Phone == phone);
    }

    public async Task Add(Guid roleId, string firstName, string lastName, string? middleName,
        string userName, string password, string email, string phone, bool isActive = true)
    {
        var userEntity = new UserEntity
        {
            RoleId = roleId,
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            UserName = userName,
            PasswordHash = password,
            Email = email,
            Phone = phone,
            IsActive = isActive
        };

        await _dbContext.Users
            .AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Guid id, Guid roleId,
        string firstName, string lastName, string? middleName,
        string userName, string password, string email, string phone,
        bool isActive = true)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(r => r
                .SetProperty(u => u.RoleId, roleId)
                .SetProperty(u => u.FirstName, firstName)
                .SetProperty(u => u.LastName, lastName)
                .SetProperty(u => u.MiddleName, middleName)
                .SetProperty(u => u.UserName, userName)
                .SetProperty(u => u.PasswordHash, password)
                .SetProperty(u => u.Email, email)
                .SetProperty(u => u.Phone, phone)
                .SetProperty(u => u.IsActive, isActive));
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }
}
