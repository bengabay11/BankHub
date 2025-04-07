using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BL.Core;
using BL.Core.Exceptions;
using Dal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransfersController(BankBase bank, UserManager<User> userManager) : ControllerBase
{
    private readonly BankBase bank = bank;
    private readonly UserManager<User> userManager = userManager;

    private async Task<User> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await userManager.FindByIdAsync(userId);

    }

    [HttpGet]
    public async Task<ActionResult<List<Transfer>>> GetUserTransfers()
    {
        var currentUser = await GetCurrentUser();
        try
        {
            return bank.GetUserTransfers(currentUser.Id).ToList();
        }
        catch (UserNotFoundException e)
        {
            return NotFound(new { e.Message });
        }
    }

    [HttpGet("{transferId}")]
    public async Task<ActionResult<Transfer>> GetUserTransfer(Guid transferId)
    {
        var currentUser = await GetCurrentUser();
        try
        {
            return bank.GetUserTransfers(currentUser.Id, transferId);
        }
        catch (Exception e) when (e is NotUserTransferException || e is TransferNotFoundException)
        {
            return NotFound(new { e.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Transfer>> CreateTransfer([FromBody] CreateTransferBody createTransferBody)
    {
        var currentUser = await GetCurrentUser();

        try
        {
            Transfer transfer = bank.Transfer(
                currentUser.Id,
                createTransferBody.TakerUserId,
                createTransferBody.Amount
            );
            return CreatedAtAction(nameof(GetUserTransfer), new { transferId = transfer.Id }, transfer);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(new { e.Message });
        }
        catch (SelfTransferException e)
        {
            return Conflict(new { e.Message });
        }
        catch (NotPositiveAmountException e)
        {
            return BadRequest(new { e.Message });
        }
    }
}
