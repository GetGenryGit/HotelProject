using HotelProject.Application.Requests.Booking;
using HotelProject.Application.Responses.Booking;
using HotelProject.Shared.Wrapper;

namespace Client.Infrastructure.Managers.BookingManager;

public interface IBookingManager
{
    Task<IResult<BookingResponse>> GetByBookingId(string id);    
    Task<IResult<List<BookingResponse>>> GetByUserId(string userId);
    Task<IResult> Update(string bookingId, BookingRequest model);
    Task<IResult> Add(BookingRequest model);
}
