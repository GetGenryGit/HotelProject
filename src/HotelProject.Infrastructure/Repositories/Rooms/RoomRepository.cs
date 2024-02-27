using HotelProject.Application.Interfaces.Repositories.Rooms;
using HotelProject.Domain;
using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Infrastructure.Repositories.Rooms;

public class RoomRepository(HotelDbContext dbContext)
    : IRoomsRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<List<RoomEntity>> Get()
    {
        return await _dbContext.Rooms
            .OrderBy(r => r.Name)
            .Include(r => r.Type)
            .ToListAsync();
    }

    public async Task<RoomEntity?> GetById(Guid id)
    {
        return await _dbContext.Rooms
            .Include(r => r.Type)
            .FirstOrDefaultAsync(r => r.Id == id);   
    }

    public async Task<RoomEntity?> GetByName(string name)
    {
        return await _dbContext.Rooms
            .Include(r => r.Type)
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task Add(Guid typeId, string name, int floorLevel)
    {
        var roomEntity = new RoomEntity
        {
            TypeId = typeId,
            Name = name,
            FloorLevel = floorLevel
        };

        await _dbContext.Rooms
            .AddAsync(roomEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.Rooms
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task Update(Guid id, Guid typeid,
        string name, int floorLevel,
        bool isCleaned, bool isActive)
    {
        await _dbContext.Rooms
            .Where(r => r.Id == id)
            .ExecuteUpdateAsync(r => r
                .SetProperty(r => r.TypeId, typeid)
                .SetProperty(r => r.Name, name)
                .SetProperty(r => r.FloorLevel, floorLevel)
                .SetProperty(r => r.IsCleaned, isCleaned)
                .SetProperty(r => r.IsActive, isActive));
    }
}
