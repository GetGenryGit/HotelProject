using HotelProject.Application.Interfaces.Services.Rooms;
using HotelProject.Application.Requests.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers.Room;

[ApiController]
[Route("api/room")]
public class RoomController(IRoomsService roomsService)
    : ControllerBase
{
    private readonly IRoomsService _roomsService = roomsService;

    /// <summary>
    /// Get All Rooms by dates
    /// </summary>
    /// <param name="checkinDate"></param>
    /// <param name="checkoutDate"></param>
    /// <returns>Status 200 OK</returns>
    [HttpGet("{checkinDate}/{checkoutDate}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByDates(DateTime checkinDate, DateTime checkoutDate)
    {
        var response = await _roomsService.GetRoomsByDates(checkinDate, checkoutDate);
        return Ok(response);
    }

    /// <summary>
    /// Delete Room
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _roomsService.GetById(id, DateTime.UtcNow, DateTime.UtcNow);
        return Ok(response);
    }

    /// <summary>
    /// Get Room by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="checkinDate"></param>
    /// <param name="checkoutDate"></param>
    /// <returns>Status 200 OK</returns>
    [HttpGet("{id}/{checkinDate}/{checkoutDate}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, DateTime checkinDate, DateTime checkoutDate)
    {
        var response = await _roomsService.GetById(id, checkinDate, checkoutDate);
        return Ok(response);
    }

    /// <summary>
    /// Add Room
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Add(RoomRequest model)
    {
        var response = await _roomsService.AddRoom(model.TypeId,
            model.Name, model.FloorLevel);
        return Ok(response);
    }

    /// <summary>
    /// Update Room
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPatch("{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Update(Guid id, RoomRequest model)
    {
        var response = await _roomsService.UpdateRoom(id, model.TypeId,
            model.Name, model.FloorLevel, model.IsCleaned, model.IsActive);
        return Ok(response);
    }


    /// <summary>
    /// Delete Room
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _roomsService.DeleteRoom(id);
        return Ok(response);
    }
}
