using HotelProject.Domain.Entities;

namespace HotelProject.Application.Interfaces.Repositories.Rooms;

public interface IRoomTypeRepository
{
    Task<List<RoomTypeEntity>> Get();
    Task<RoomTypeEntity?> GetByName(string name);

    Task Add(string name, string description, decimal priceOnePerson, int maxPersons);
    Task Delete(Guid id);
    Task Update(Guid id, string name, string description, decimal priceOnePerson, int maxPersons);
}
