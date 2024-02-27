using HotelProject.Application.Responses.Room;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Application.Interfaces.Services.Rooms;

public interface IRoomsService
{
    #region [Rooms]
    Task<Result<List<RoomResponse>>> GetRoomsByDates(DateTime checkinDate, DateTime checkoutDate);
    Task<Result<RoomResponse>> GetById(Guid id, DateTime checkinDate, DateTime checkoutDate);
    Task<Result> AddRoom(Guid typeId, string name, int floorLevel);
    Task<Result> UpdateRoom(Guid id, Guid typeId,
        string name, int floorLevel,
        bool isCleaned, bool isActive);
    Task<Result> DeleteRoom(Guid id);
    #endregion

    #region [RoomTypes]
    Task<Result<List<RoomTypeResponse>>> GetRoomTypes();

    Task<Result> AddRoomType(string name, string description,
        decimal priceOnePerson, int maxPersons);
    Task<Result> UpdateRoomType(Guid id,
        string name, string description,
        decimal priceOneNight, int maxPersons);
    Task<Result> DeleteRoomType(Guid id);
    #endregion
}
