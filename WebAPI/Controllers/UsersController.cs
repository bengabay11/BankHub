using Microsoft.AspNetCore.Mvc;
using Dal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Models;
using WebAPI.Utils;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(UserManager<User> userManager) : ControllerBase
{
    private readonly UserManager<User> userManager = userManager;

    [HttpGet("current")]
    public async Task<ActionResult<ExtendedUserResponse>> GetCurrentUserInfo()
    {
        var user = await UserUtils.GetCurrentUser(User, userManager);
        var roles = await userManager.GetRolesAsync(user);
        
        var permissions = new List<Permission> { Permission.ViewDashboard };
        if (roles.Contains("Admin"))
        {
            permissions.Add(Permission.ViewAdminDashboard);
        }

        return ExtendedUserResponse.FromUser(user, [.. permissions]);
    }

    [HttpGet]
    public ActionResult<List<UserResponse>> GetAllUsers()
    {
        var users = userManager.Users.ToList();
        return users.Select(UserResponse.FromUser).ToList();
    }
}
