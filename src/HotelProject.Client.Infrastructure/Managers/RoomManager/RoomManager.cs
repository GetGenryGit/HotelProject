using Client.Infrastructure.Extensions;
using Client.Infrastructure.Routes.Rooms;
using Client.Infrastructure.Services;
using HotelProject.Application.Responses.Room;
using HotelProject.Shared.Wrapper;

namespace Client.Infrastructure.Managers.RoomManager;

public class RoomManager(IHttpService httpService)
    : IRoomManager
{
    private readonly IHttpService _httpService = httpService;

    public async Task<IResult<List<RoomResponse>>> GetDataAsync(DateTime checkinDate, DateTime checkoutDate)
    {
        var response = await _httpService.Get(Routes.Rooms.RoomEndpoints.GetByDates(checkinDate, checkoutDate));
        var data = await response.ToResult<List<RoomResponse>>();
        return data;
    }

    public async Task<IResult<RoomResponse>> GetRoom(string id)
    {
        var response = await _httpService.Get(RoomEndpoints.Get(id));
        var data = await response.ToResult<RoomResponse>();
        return data;
    }
}
