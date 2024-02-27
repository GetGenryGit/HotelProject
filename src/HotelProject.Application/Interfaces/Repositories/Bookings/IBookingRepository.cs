using HotelProject.Domain.Entities;

namespace HotelProject.Application.Interfaces.Repositories.Bookings;

public interface IBookingRepository
{
    Task<List<BookingEntity>> Get();
    Task<List<BookingEntity>> GetByUserId(Guid id);
    Task<List<BookingEntity>> GetByDates(DateTime checkinDate, DateTime checkoutDate);
    Task<List<BookingEntity>> GetByDatesForRoom(Guid roomId,
        DateTime checkinDate, DateTime checkoutDate);
    Task<BookingEntity?> GetBookingById(Guid id);


    Task Add(Guid roomId, Guid userId, Guid statusId,
        DateTime checkinDate, DateTime checkoutDate,
        decimal totalPrice, int personCount);
    Task Update(Guid id, Guid roomId, Guid statusId,
        DateTime checkinDate, DateTime checkoutDate,
        decimal totalPrice, int personCount);
    Task Delete(Guid id);
}
