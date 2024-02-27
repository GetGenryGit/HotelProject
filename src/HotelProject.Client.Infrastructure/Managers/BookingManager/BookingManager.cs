using Client.Infrastructure.Extensions;
using Client.Infrastructure.Routes.Bookings;
using Client.Infrastructure.Services;
using HotelProject.Application.Requests.Booking;
using HotelProject.Application.Responses.Booking;
using HotelProject.Shared.Wrapper;

namespace Client.Infrastructure.Managers.BookingManager;

public class BookingManager(IHttpService httpService)
    : IBookingManager
{
    private readonly IHttpService _httpService = httpService;

    public async Task<IResult> Add(BookingRequest model)
    {
        var response = await _httpService.Post(BookingEndpoints.Add, model);
        var data = await response.ToResult();
        return data;
    }

    public async Task<IResult<BookingResponse>> GetByBookingId(string id)
    {
        var response = await _httpService.Get(BookingEndpoints.GetByBookingId(id));
        var data = await response.ToResult<BookingResponse>();
        return data;
    }

    public async Task<IResult<List<BookingResponse>>> GetByUserId(string userId)
    {
        var response = await _httpService.Get(BookingEndpoints.GetByUserId(userId));
        var data = await response.ToResult<List<BookingResponse>>();
        return data;
    }

    public async Task<IResult> Update(string bookingId, BookingRequest model)
    {
        var response = await _httpService.Patch(BookingEndpoints.Update(bookingId), model);
        var data = await response.ToResult();
        return data;
    }
}
