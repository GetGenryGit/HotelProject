using HotelProject.Application.Interfaces.Repositories.Bookings;
using HotelProject.Application.Interfaces.Repositories.Identity;
using HotelProject.Application.Interfaces.Repositories.Rooms;
using HotelProject.Application.Interfaces.Services.Booking;
using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Application.Interfaces.Services.Rooms;
using HotelProject.Infrastructure.Helpers;
using HotelProject.Infrastructure.Repositories.Bookings;
using HotelProject.Infrastructure.Repositories.Identity;
using HotelProject.Infrastructure.Repositories.Rooms;
using HotelProject.Infrastructure.Services.Bookings;
using HotelProject.Infrastructure.Services.Identity;
using HotelProject.Infrastructure.Services.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace HotelProject.Infrastructure.Extenstions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IUsersRepository, UsersRepository>()
            .AddTransient<IRefreshSessionsRepository, RefreshSessionsRepository>()
            .AddTransient<IRolesRepository, RolesRepository>()
            .AddTransient<IRoomsRepository, RoomRepository>()
            .AddTransient<IRoomTypeRepository, RoomTypeRepository>()
            .AddTransient<IBookingRepository, BookingRepository>()
            .AddTransient<IBookingStatusRepository, BookingStatusRepository>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IRoleService, RoleService>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IRoomsService, RoomsService>()
            .AddTransient<IBookingsService, BookingsService>();
    }

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        return services
            .AddTransient<IPasswordHasher, PasswordHasher>();
    }


}
