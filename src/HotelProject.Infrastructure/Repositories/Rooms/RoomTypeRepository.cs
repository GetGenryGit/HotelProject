using HotelProject.Application.Interfaces.Repositories.Rooms;
using HotelProject.Domain;
using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Infrastructure.Repositories.Rooms;

internal class RoomTypeRepository(HotelDbContext dbContext)
    : IRoomTypeRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<List<RoomTypeEntity>> Get()
    {
        return await _dbContext.RoomTypes
            .ToListAsync();
    }

    public async Task<RoomTypeEntity?> GetByName(string name)
    {
        return await _dbContext.RoomTypes
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task Add(string name, string description, decimal priceOneNight, int maxPersons)
    {
        var roomTypeEntity = new RoomTypeEntity
        {
            Name = name,
            Description = description,  
            PriceOneNight = priceOneNight,
            MaxPersons = maxPersons
        };

        await _dbContext.RoomTypes
            .AddAsync(roomTypeEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.RoomTypes
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task Update(Guid id, 
        string name, string description, decimal priceOneNight, int maxPersons)
    {
        await _dbContext.RoomTypes
            .Where(rt => rt.Id == id)
            .ExecuteUpdateAsync(rt => rt
                .SetProperty(rt => rt.Name, name)
                .SetProperty(rt => rt.Description, description)
                .SetProperty(rt => rt.PriceOneNight, priceOneNight)
                .SetProperty(rt => rt.MaxPersons, maxPersons));
    }
}
