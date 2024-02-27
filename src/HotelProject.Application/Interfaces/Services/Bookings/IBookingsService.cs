using HotelProject.Application.Responses.Booking;
using HotelProject.Shared.Wrapper;

namespace HotelProject.Application.Interfaces.Services.Booking;

public interface IBookingsService
{
    #region [Bookings]
    Task<Result<List<BookingResponse>>> GetAllBookings();
    Task<Result<List<BookingResponse>>> GetAllBookingByUserId(Guid id);
    Task<Result<BookingResponse>> GetBookingById(Guid id);

    Task<Result> AddBooking(Guid userId, string roomName, string statusName,
        DateTime checkinDate, DateTime checkoutDate, int personCount);
    Task<Result> UpdateBooking(Guid id, string roomName, string statusName,
        DateTime     checkinDate, DateTime checkoutDate, int personCount, bool isNeedCleaningRoom);
    #endregion

    #region [BookingStatuses]
    Task<Result<List<BookingStatusResponse>>> GetBookingStatuses();
    Task<Result> AddBookingStatus(string name, string description);
    Task<Result> UpdateBookingStatus(Guid id, string name, string description);
    Task<Result> DeleteBookingStatus(Guid id);
    #endregion

}
