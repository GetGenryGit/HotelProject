using HotelProject.Application.Interfaces.Repositories.Identity;
using HotelProject.Domain;
using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Infrastructure.Repositories.Identity;

public class RolesRepository(HotelDbContext dbContext)
    : IRolesRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<List<RoleEntity>> Get()
    {
        return await _dbContext.Roles
            .ToListAsync();
    }

    public async Task<RoleEntity?> GetByName(string name)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task Add(string name, string description)
    {
        var roleEntity = new RoleEntity
        {
            Name = name,
            Description = description
        };

        await _dbContext.Roles
            .AddAsync(roleEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.Roles
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }


    public async Task Update(Guid id, string name, string description)
    {
        await _dbContext.Roles
            .Where(r => r.Id == id)
            .ExecuteUpdateAsync(r => r
                .SetProperty(r => r.Name, name)
                .SetProperty(r => r.Description, description));
    }
}
