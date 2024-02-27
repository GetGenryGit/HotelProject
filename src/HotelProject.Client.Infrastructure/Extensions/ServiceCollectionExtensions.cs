using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using Client.Infrastructure.Authentication;
using Client.Infrastructure.Managers.AuthenticationManager;
using Client.Infrastructure.Managers.BookingManager;
using Client.Infrastructure.Managers.RoomManager;
using Client.Infrastructure.Managers.UserManager;
using Client.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services
            .AddBlazoredToast()
            .AddAuthorizationCore()
            .AddBlazoredSessionStorage()
            .AddBlazoredLocalStorage()
            .AddScoped<HotelStateProvider>()
            .AddScoped<AuthenticationStateProvider, HotelStateProvider>();
         services.AddScoped(sp => sp
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("Hotel.API"))
         .AddTransient<IHttpService, HttpService>();
        return services;
    }

    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        return services
            .AddTransient<IRoomManager, RoomManager>()
            .AddTransient<IAuthenticationManager, AuthenticationManager>()
            .AddTransient<IUserManager, UserManager>()
            .AddTransient<IBookingManager, BookingManager>();
    }
}
