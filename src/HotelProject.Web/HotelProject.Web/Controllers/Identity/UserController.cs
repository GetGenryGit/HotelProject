using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Application.Requests.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers.Identity;

[ApiController]
[Route("api/identity/user")]
public class UserController(IUserService userService)
    : ControllerBase
{
    private readonly IUserService _userService = userService;

    /// <summary>
    /// Get Users
    /// </summary>
    /// <returns>Status 200 OK</returns>
    [HttpGet]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _userService.GetAllAsync();
        return Ok(response);
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var response = await _userService.GetUserById(id);
        return Ok(response);
    }

    /// <summary>
    /// Get User Role by UserId
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpGet("role/{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> GetUserRoleByUserId(Guid id)
    {
        var response = await _userService.GetUserRoleByUserId(id);
        return Ok(response);
    }



    /// <summary>
    /// Register a User
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(RegisterRequest model)
    {
        var response = await _userService.RegisterUser(model.FirstName, model.LastName, model.MiddleName,
            model.UserName, model.Password, model.Email, model.Phone);
        return Ok(response);
    }


    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Patch(Guid id, UserRequest model)
    {
        var response = await _userService.UpdateUser(id, model.FirstName, model.LastName, model.MiddleName,
            model.UserName, model.Password, model.Email, model.Phone, model.RoleName, model.IsActive);
        return Ok(response);
    }

}
