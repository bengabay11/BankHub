using WebAPI.Models;
using BL.Core;
using BL.Core.Exceptions;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(BankBase bank, UserManager<User> userManager) : ControllerBase
{
    private readonly BankBase _bank = bank;
    private readonly UserManager<User> _userManager = userManager;

    [HttpPost("register")]
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

    [Authorize] // TODO: authorize that only for admin
    [HttpGet]
    public ActionResult<List<User>> GetAll() => _bank.GetAllUsers().ToList();

    [Authorize] // TODO: authorize that only for admin
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(Guid id)
    {
        try
        {
            return _bank.GetUser(id);
        }
        catch (NotUserTransferException e)
        {
            return NotFound(new { e.Message });
        }
    }

    [Authorize] // TODO: authorize that only for admin
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        // Ignoring result to check if the user exists before deleting it
        GetUser(id);
        _bank.DeleteUser(id);
        return Ok(new { Message = "User deleted successfully" });
    }
}
