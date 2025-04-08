using BL.Core;
using BL.Core.Exceptions;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("admin/[controller]")]
public class UsersController(BankBase bank) : ControllerBase
{
    private readonly BankBase _bank = bank;

    [HttpGet]
    public ActionResult<List<User>> GetAll() => _bank.GetAllUsers().ToList();

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

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        // Ignoring result to check if the user exists before deleting it
        GetUser(id);
        _bank.DeleteUser(id);
        return Ok(new { Message = "User deleted successfully" });
    }
}
