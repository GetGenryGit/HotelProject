using HotelProject.Application.Interfaces.Repositories.Identity;
using HotelProject.Domain;
using HotelProject.Domain.Entities;
using HotelProject.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Infrastructure.Repositories.Identity;

public class RefreshSessionsRepository(HotelDbContext dbContext)
    : IRefreshSessionsRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<RefreshSessionEntity?> GetByRefreshToken(string refreshToken)
    {
        return await _dbContext.RefreshSessions
            .FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
    }

    public async Task Add(Guid userId, string refreshToken)
    {
        var refreshSession = new RefreshSessionEntity
        {
            UserId = userId,
            RefreshToken = refreshToken
        };

        await _dbContext.RefreshSessions
            .AddAsync(refreshSession);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(string refreshToken)
    {
        await _dbContext.RefreshSessions
             .Where(s => s.RefreshToken == refreshToken)
             .ExecuteDeleteAsync();
    }

    public async Task<List<RefreshSessionEntity>> Get()
    {
        return await _dbContext.RefreshSessions
            .ToListAsync();
    }

    public async Task DeleteAll()
    {
        await _dbContext.RefreshSessions
            .ExecuteDeleteAsync();
    }
}
