using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BL.Core;
using Dal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BL.Core.Exceptions;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BalanceController(BankBase bank, UserManager<User> userManager) : ControllerBase
{
    private readonly BankBase _bank = bank;

    private async Task<User> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await userManager.FindByIdAsync(userId);

    }

    [HttpGet]
    public async Task<ActionResult<GetUserBalanceResponse>> GetUserBalance()
    {
        var currentUser = await GetCurrentUser();
        return Ok(new GetUserBalanceResponse { Balance = currentUser.Balance });
    }

    [HttpPut("Withdraw")]
    public async Task<ActionResult> Withdraw([FromBody] WithdrawBody withdrawBody)
    {
        try
        {
            var currentUser = await GetCurrentUser();
            _bank.Withdraw(currentUser.Id, withdrawBody.Amount);
            return Ok(new { Message = $"Successfully withdrew '{withdrawBody.Amount}' from the account" });
        }
        catch (Exception e) when (e is NotPositiveAmountException || e is InsufficientBalanceException)
        {
            return BadRequest(new { e.Message, Reason = nameof(e) });
        }
    }

    [HttpPut("Deposit")]
    public async Task<ActionResult> Deposit([FromBody] DepositBody depositBody)
    {
        try
        {
            var currentUser = await GetCurrentUser();
            _bank.Deposit(currentUser.Id, depositBody.Amount);
            return Ok(new { Message = $"'{depositBody.Amount}' were successfully deposited into the account." });
        }
        catch (NotPositiveAmountException e)
        {
            return BadRequest(new { e.Message });
        }
    }

    [HttpGet("History")]
    public async Task<ActionResult<List<BalanceUpdate>>> History()
    {
        var currentUser = await GetCurrentUser();
        return _bank.GetUserBalanceHistory(currentUser.Id).ToList();
    }
}
