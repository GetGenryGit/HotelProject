using HotelProject.Domain.Entities;

namespace HotelProject.Application.Interfaces.Repositories.Identity;

public interface IRefreshSessionsRepository
{
    Task<List<RefreshSessionEntity>> Get();
    Task<RefreshSessionEntity?> GetByRefreshToken(string refreshToken);
    Task Add(Guid userId, string refreshToken);
        Task DeleteAll();
    Task Delete(string refreshToken);
}
