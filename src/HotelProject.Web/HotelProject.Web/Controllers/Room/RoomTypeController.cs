using HotelProject.Application.Interfaces.Services.Rooms;
using HotelProject.Application.Requests.Room;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers.Room;

[ApiController]
[Route("api/room/type")]
public class RoomTypeController(IRoomsService roomsService)
    : ControllerBase
{
    private readonly IRoomsService _roomsService = roomsService;

    /// <summary>
    /// Get Room Types
    /// </summary>
    /// <returns>Status 200 OK</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _roomsService.GetRoomTypes();
        return Ok(response);
    }

    /// <summary>
    /// Add Room Type
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost]
    public async Task<IActionResult> Add(RoomTypeRequest model)
    {
        var response = await _roomsService.AddRoomType(model.Name, 
            model.Description, model.PriceOneNight, model.MaxPersons);
        return Ok(response);
    }

    /// <summary>
    /// Update Room Type
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, RoomTypeRequest model)
    {
        var response = await _roomsService.UpdateRoomType(id,
            model.Name, model.Description, model.PriceOneNight, model.MaxPersons);
        return Ok(response);
    }


    /// <summary>
    /// Delete Room Type
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _roomsService.DeleteRoomType(id);
        return Ok(response);
    }
}
