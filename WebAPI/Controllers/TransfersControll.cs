using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BL.Core;
using BL.Core.Exceptions;
using Dal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebAPI.Utils;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransfersController(BankBase bank, UserManager<User> userManager) : ControllerBase
{
    private readonly BankBase bank = bank;
    private readonly UserManager<User> userManager = userManager;

    [HttpGet]
    public async Task<ActionResult<List<TransferResponse>>> GetUserTransfers()
    {
        var currentUser = await UserUtils.GetCurrentUser(User, userManager);
        return bank.GetUserTransfers(currentUser.Id)
                   .OrderByDescending(t => t.At)
                   .Select(TransferResponse.FromTransfer)
                   .ToList();
    }

    [HttpGet("{transferId}")]
    public async Task<ActionResult<TransferResponse>> GetUserTransfer(Guid transferId)
    {
        var currentUser = await UserUtils.GetCurrentUser(User, userManager);
        try
        {
            var transfer = bank.GetUserTransfer(currentUser.Id, transferId);
            return TransferResponse.FromTransfer(transfer);
        }
        catch (Exception e) when (e is NotUserTransferException || e is TransferNotFoundException)
        {
            return NotFound(new { e.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<TransferResponse>> CreateTransfer([FromBody] CreateTransferBody createTransferBody)
    {
        var currentUser = await UserUtils.GetCurrentUser(User, userManager);

        try
        {
            Transfer transfer = bank.Transfer(
                currentUser.Id,
                createTransferBody.TakerUserId,
                createTransferBody.Amount
            );
            return CreatedAtAction(
                nameof(GetUserTransfer),
                new { transferId = transfer.Id },
                TransferResponse.FromTransfer(transfer)
            );
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
