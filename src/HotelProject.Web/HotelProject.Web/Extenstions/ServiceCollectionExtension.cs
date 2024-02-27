using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Domain;
using HotelProject.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HotelProject.Web.Extenstions;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddJWTAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration.GetSection("JWTSettings:Issuer").Value
            ?? throw new ArgumentException("Issuer is not found");
        var audience = configuration.GetSection("JWTSettings:Audience").Value
            ?? throw new ArgumentException("Audience key is not found");
        var secretKey = configuration.GetSection("JWTSettings:SecretKey").Value 
            ?? throw new ArgumentException("Secret key is not found");
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new()
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = signinKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
        services.AddAuthorization();

        return services;
    }

    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
        });
    }
}
