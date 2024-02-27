using HotelProject.Application.Interfaces.Repositories.Identity;
using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Application.Responses.Identity;
using HotelProject.Domain.Entities;
using HotelProject.Infrastructure.Helpers;
using HotelProject.Shared.Wrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HotelProject.Infrastructure.Services.Identity;

public class TokenService(IConfiguration configuration,
    IRefreshSessionsRepository refreshSessionsRepository,
    IUsersRepository usersRepository, IPasswordHasher passwordHasher)
    : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IRefreshSessionsRepository _refreshSessionsRepository = refreshSessionsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<TokenResponse>> Login(string username, string password)
    {
        var user = await _usersRepository.GetUserByUserName(username);
        if (user == null)
        {
            return await Result<TokenResponse>.FailAsync("Неправильный логин!");
        }
        var result = _passwordHasher.Verify(password, user.PasswordHash);
        if (!result)
        {
            return await Result<TokenResponse>.FailAsync("Неправильный пароль!");
        }

        var authToken = await generateJwtAsync(user); 
        var refreshToken = generateRefreshToken();
        await _refreshSessionsRepository.Add(user.Id, refreshToken);
        var tokenResponse = new TokenResponse
        {
            AuthToken = authToken,
            RefreshToken = refreshToken    
        };

        return await Result<TokenResponse>.SuccessAsync(tokenResponse);
    }

    public async Task<Result<TokenResponse>> RefreshToken(string refreshToken)
    {
        var refreshSession = await _refreshSessionsRepository.GetByRefreshToken(refreshToken);
        if (refreshSession == null)
        {
            return await Result<TokenResponse>
                .FailAsync("Сессии не существует, необходимо вновь пройти аунтификацию!");
        }

        if (refreshSession.Expired < DateTime.UtcNow)
        {
            await _refreshSessionsRepository.Delete(refreshToken);
            return await Result<TokenResponse>
                .FailAsync("Сессия устарела, необходимо вновь пройти аунтификацию!");
        }

        var user = await _usersRepository.GetById(refreshSession.UserId);
        if (user == null)
        {
            return await Result<TokenResponse>
                .FailAsync("Пользователя с данным токеном не существует!");
        }

        var newAuthToken = await generateJwtAsync(user);
        var newRefreshToken = generateRefreshToken();

        await _refreshSessionsRepository.Delete(refreshToken);
        await _refreshSessionsRepository.Add(user.Id, newRefreshToken);

        var tokenResponse = new TokenResponse
        {
            AuthToken = newAuthToken,
            RefreshToken = newRefreshToken
        };

        return await Result<TokenResponse>.SuccessAsync(tokenResponse);
    }

    public async Task<Result<string>> RevokeToken(string refreshToken)
    {
        var refreshSession = await _refreshSessionsRepository.GetByRefreshToken(refreshToken);
        if (refreshSession == null)
        {
            return await Result<string>
                .FailAsync("Сессии уже не существует, необходимо вновь пройти аунтификацию!");
        }

        await _refreshSessionsRepository.Delete(refreshToken);
        return await Result<string>
            .SuccessAsync("Сессия успешно отозвана!");
    }

    private async Task<string> generateJwtAsync(UserEntity user)
    {
        var token = generateEncryptedToken(
            getSigningCredentials(), await getClaimsAsync(user));
        return token;
    }

    private async Task<IEnumerable<Claim>> getClaimsAsync(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new("user_guid", user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserName.ToString()),
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.Name),
            new(ClaimTypes.MobilePhone, user.Phone)
        };

        return claims;
    }

    private string generateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var issuer = configuration.GetSection("JWTSettings:Issuer").Value 
            ?? throw new ArgumentException("Issuer is not found");
        var audience = configuration.GetSection("JWTSettings:Audience").Value 
            ?? throw new ArgumentException("Audience key is not found");

        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(5),
           issuer: issuer,
           audience: audience,
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }

    private string generateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private SigningCredentials getSigningCredentials()
    {
        var secretKey = _configuration.GetSection("JWTSettings:SecretKey").Value
            ?? throw new ArgumentException("Secret key is not found");
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        return new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
    }
}
