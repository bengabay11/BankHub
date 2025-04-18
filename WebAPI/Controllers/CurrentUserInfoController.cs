using Microsoft.AspNetCore.Mvc;
using BL.Core;
using Dal.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CurrentUserInfoController(BankBase bank, UserManager<User> userManager) : ControllerBase
{
    private readonly BankBase bank = bank;
    private readonly UserManager<User> userManager = userManager;

    private async Task<User> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await userManager.FindByIdAsync(userId);

    }

    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUserInfo()
    {
        return UserResponse.FromUser(await GetCurrentUser());
    }
}
