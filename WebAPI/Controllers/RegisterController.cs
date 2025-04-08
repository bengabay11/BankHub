using WebAPI.Models;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController(UserManager<User> userManager) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserBody model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            Type = model.UserType,
            DisplayName = model.DisplayName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "Registration successful" });
        }

        return BadRequest(new { result.Errors });
    }
}
