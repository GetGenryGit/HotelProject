using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Application.Requests.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers.Identity;

[ApiController]
[Route("api/identity/token")]
public class TokenController(ITokenService tokenService)
    : ControllerBase
{
    private readonly ITokenService _tokenService = tokenService;

    /// <summary>
    /// Get Token (Login, Password)
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Get(TokenRequest model)
    {
        var response = await _tokenService.Login(model.UserName, model.Password);
        return Ok(response);
    }

    /// <summary>
    /// Revoke Token (AuthToken, RefreshToken)
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost("revoke")]
    [AllowAnonymous]
    public async Task<IActionResult> Revoke(RefreshTokenRequest model)
    {
        var response = await _tokenService.RevokeToken(model.RefreshToken);
        return Ok(response);
    }

    /// <summary>
    /// Refresh Expired AuthToken (AuthToken, RefreshToken)
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPut("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest model)
    {
        var response = await _tokenService.RefreshToken(model.RefreshToken);
        return Ok(response);
    }

}
