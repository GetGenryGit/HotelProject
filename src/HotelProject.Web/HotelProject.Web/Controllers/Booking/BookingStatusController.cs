using HotelProject.Application.Interfaces.Services.Booking;
using HotelProject.Application.Requests.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers.Booking;

[ApiController]
[Route("api/booking/status")]
public class BookingStatusController(IBookingsService bookingsService)
    : ControllerBase
{
    private readonly IBookingsService _bookingsService = bookingsService;

    /// <summary>
    /// Get Booking Statuses
    /// </summary>
    /// <returns>Status 200 OK</returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var response = await _bookingsService.GetBookingStatuses();
        return Ok(response);
    }

    /// <summary>
    /// Add Booking Status
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Add(BookingStatusRequest model)
    {
        var response = await _bookingsService.AddBookingStatus(model.Name, model.Description);
        return Ok(response);
    }

    /// <summary>
    /// Update Booking Status
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPatch("{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Update(Guid id, BookingStatusRequest model)
    {
        var response = await _bookingsService.UpdateBookingStatus(id,
            model.Name, model.Description);
        return Ok(response);
    }


    /// <summary>
    /// Delete Booking Type
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _bookingsService.DeleteBookingStatus(id);
        return Ok(response);
    }
}
