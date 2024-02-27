using HotelProject.Application.Interfaces.Services.Booking;
using HotelProject.Application.Requests.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers.Booking;

[ApiController]
[Route("api/booking")]
public class BookingController(IBookingsService bookingsService) 
    : ControllerBase
{
    private readonly IBookingsService _bookingsService = bookingsService;

    /// <summary>
    /// Get Bookings
    /// </summary>
    /// <returns>Status 200 OK</returns>
    [HttpGet]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _bookingsService.GetAllBookings();
        return Ok(response);
    }

    /// <summary>
    /// Get Bookings by User Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpGet("user/{id}")]
    [Authorize]
    public async Task<IActionResult> GetByUserId(Guid id)
    {
        var response = await _bookingsService.GetAllBookingByUserId(id);
        return Ok(response);
    }

    /// <summary>
    /// Get Bookings by User Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByBookingId(Guid id)
    {
        var response = await _bookingsService.GetBookingById(id);
        return Ok(response);
    }

    /// <summary>
    /// Add Booking 
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(BookingRequest model)
    {
        var response = await _bookingsService.AddBooking(model.UserId, model.RoomName,
            model.StatusName, model.CheckinDate, model.CheckoutDate, model.PersonCount);
        return Ok(response);
    }

    /// <summary>
    /// Update Booking 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, BookingRequest model)
    {
        var response = await _bookingsService.UpdateBooking(id, model.RoomName,
            model.StatusName, model.CheckinDate, model.CheckoutDate, model.PersonCount, model.IsNeedCleaningRoom);
        return Ok(response);
    }
}
