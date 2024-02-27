using HotelProject.Application.Interfaces.Repositories.Bookings;
using HotelProject.Domain;
using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Infrastructure.Repositories.Bookings;

public class BookingRepository(HotelDbContext dbContext)
    : IBookingRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task Add(Guid roomId, Guid userId, Guid statusId, 
        DateTime checkinDate, DateTime checkoutDate, decimal totalPrice, int personCount)
    {
        var bookingEnitity = new BookingEntity
        {
            RoomId = roomId,
            UserId = userId,    
            StatusId = statusId,
            CheckinDate = checkinDate,
            CheckoutDate = checkoutDate,
            TotalPrice = totalPrice,
            PersonCount = personCount
        };

        await _dbContext.Bookings
            .AddAsync(bookingEnitity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.Bookings
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<List<BookingEntity>> Get()
    {
        return await _dbContext.Bookings
            .Include(b => b.User)
            .Include(b => b.Room)
            .Include(b => b.Status)
            .OrderByDescending(b => b.Created)
            .ToListAsync();
    }

    public async Task<List<BookingEntity>> GetByDatesForRoom(Guid roomId, 
        DateTime checkinDate, DateTime checkoutDate)
    {
        return await _dbContext.Bookings
              .Where(b => b.RoomId == roomId &&
                        (b.CheckinDate > checkinDate && b.CheckoutDate < checkoutDate) ||
                        (b.CheckoutDate > checkinDate && b.CheckinDate < checkoutDate) ||
                        (b.CheckoutDate == checkinDate) || (b.CheckinDate == checkoutDate) ||
                        (b.CheckinDate == checkinDate) || (b.CheckoutDate == checkoutDate))
              .Include(b => b.Status)
              .ToListAsync();
    }

    public async Task<List<BookingEntity>> GetByDates(DateTime checkinDate, DateTime checkoutDate)
    {
        return await _dbContext.Bookings
              .Where(b => 
                        (b.CheckinDate > checkinDate && b.CheckoutDate < checkoutDate) || 
                        (b.CheckoutDate > checkinDate && b.CheckinDate < checkoutDate) ||
                        (b.CheckoutDate == checkinDate) || (b.CheckinDate == checkoutDate) ||
                        (b.CheckinDate == checkinDate) || (b.CheckoutDate == checkoutDate))
              .Include(b => b.Status)

              .ToListAsync();
    }

    public async Task<List<BookingEntity>> GetByUserId(Guid userId)
    {
        return await _dbContext.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.User)
            .Include(b => b.Room)
            .Include(b => b.Status)
            .OrderByDescending(b => b.Created)
            .ToListAsync();
    }

    public async Task Update(Guid id, Guid roomId, Guid statusId, 
        DateTime checkinDate, DateTime checkoutDate, decimal totalPrice, int personCount)
    {
        await _dbContext.Bookings
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(b => b
                .SetProperty(b => b.RoomId, roomId)
                .SetProperty(b => b.StatusId, statusId)
                .SetProperty(b => b.CheckinDate, checkinDate)
                .SetProperty(b => b.CheckoutDate, checkoutDate)
                .SetProperty(b => b.TotalPrice, totalPrice)
                .SetProperty(b => b.PersonCount, personCount));
    }

    public async Task<BookingEntity?> GetBookingById(Guid id)
    {
        return await _dbContext.Bookings
            .Include(b => b.User)
            .Include(b => b.Room)
            .Include(b => b.Status)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}
