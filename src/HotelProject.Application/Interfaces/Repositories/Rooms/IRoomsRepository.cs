using HotelProject.Domain.Entities;

namespace HotelProject.Application.Interfaces.Repositories.Rooms;

public interface IRoomsRepository
{
    Task<List<RoomEntity>> Get();
    Task<RoomEntity?> GetById(Guid id);
    Task<RoomEntity?> GetByName(string name);
    Task Add(Guid typeId, string name, int floorLevel);
    Task Update(Guid id, Guid typeId,
        string name, int floorLevel, bool isCleaned, bool isActive);
    Task Delete(Guid id);
}
