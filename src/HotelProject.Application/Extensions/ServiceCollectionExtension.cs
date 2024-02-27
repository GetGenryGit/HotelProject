using HotelProject.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HotelProject.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HotelDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(HotelDbContext)));
            });

        return services;
    }
}
