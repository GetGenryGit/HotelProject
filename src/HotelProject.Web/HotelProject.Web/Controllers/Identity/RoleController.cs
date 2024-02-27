using HotelProject.Application.Interfaces.Services.Identity;
using HotelProject.Application.Requests.Identity;
using HotelProject.Application.Responses.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Web.Controllers.Identity;

[ApiController]
[Route("api/identity/role")]
public class RoleController(IRoleService roleService)
    : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    /// <summary>
    /// Get Roles
    /// </summary>
    /// <returns>Status 200 OK</returns>
    [HttpGet]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _roleService.Get();
        return Ok(response);
    }

    /// <summary>
    /// Add role with Name and Description
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPost]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Add(RoleRequest model)
    {
        var response = await _roleService.Add(model.Name, model.Description);
        return Ok(response);    
    }

    /// <summary>
    /// Delete role by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status 200 OK</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _roleService.Delete(id);
        return Ok(response);
    }

    /// <summary>
    /// Update Role Name or Description
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status 200 OK</returns>
    [HttpPatch("{id}")]
    [Authorize(Roles = "Админ")]
    public async Task<IActionResult> Update(Guid id, RoleRequest model)
    {
        var response = await _roleService.Update(id, model.Name, model.Description);
        return Ok(response);
    }

}
