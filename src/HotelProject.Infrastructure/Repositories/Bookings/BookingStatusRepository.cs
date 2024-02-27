using HotelProject.Application.Interfaces.Repositories.Bookings;
using HotelProject.Domain;
using HotelProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Infrastructure.Repositories.Bookings;

public class BookingStatusRepository(HotelDbContext dbContext)
    : IBookingStatusRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task Add(string name, string descritption)
    {
        var bookingStatusEnitity = new BookingStatusEntity
        {
            Name = name,
            Description = descritption
        };

        await _dbContext.BookingStatuses
            .AddAsync(bookingStatusEnitity);
        await _dbContext.SaveChangesAsync();    
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.BookingStatuses
            .Where(bs => bs.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<List<BookingStatusEntity>> Get()
    {
        return await _dbContext.BookingStatuses
            .ToListAsync(); 
    }

    public async Task<BookingStatusEntity?> GetByName(string name)
    {
        return await _dbContext.BookingStatuses
            .FirstOrDefaultAsync(bs => bs.Name == name);
    }

    public async Task Update(Guid id, string name, string descritption)
    {
        await _dbContext.BookingStatuses
            .Where(bs => bs.Id == id)
            .ExecuteUpdateAsync(bs => bs
                .SetProperty(bs => bs.Name, name)
                .SetProperty(bs => bs.Description, descritption));
    }
}
