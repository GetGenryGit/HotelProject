using HotelProject.Application.Responses.Room;
using HotelProject.Shared.Wrapper;

namespace Client.Infrastructure.Managers.RoomManager;

public interface IRoomManager
{
    Task<IResult<List<RoomResponse>>> GetDataAsync(DateTime checkinDate, DateTime checkoutDate);
    Task<IResult<RoomResponse>> GetRoom(string id);
}
