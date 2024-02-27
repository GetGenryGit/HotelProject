using HotelProject.Domain.Entities;

namespace HotelProject.Application.Interfaces.Repositories.Bookings;

public interface IBookingStatusRepository
{
    Task<List<BookingStatusEntity>> Get();
    Task<BookingStatusEntity?> GetByName(string name);

    Task Add(string name, string descritption);
    Task Update(Guid id, string name, string descritption);
    Task Delete(Guid id);
}
