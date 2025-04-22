using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BL.Core;
using Dal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BL.Core.Exceptions;
using WebAPI.Utils;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BalanceController(BankBase bank, UserManager<User> userManager) : ControllerBase
{
    private readonly BankBase _bank = bank;

    [HttpGet]
    public async Task<ActionResult<GetUserBalanceResponse>> GetUserBalance()
    {
        var currentUser = await UserUtils.GetCurrentUser(User, userManager);
        return Ok(new GetUserBalanceResponse { Balance = currentUser.Balance });
    }

    [HttpPut("Withdraw")]
    public async Task<ActionResult<BalanceUpdateResponse>> Withdraw([FromBody] WithdrawBody withdrawBody)
    {
        try
        {
            var currentUser = await UserUtils.GetCurrentUser(User, userManager);
            var balanceUpdate = _bank.Withdraw(currentUser.Id, withdrawBody.Amount);
            return Ok(BalanceUpdateResponse.FromBalanceUpdate(balanceUpdate));
        }
        catch (Exception e) when (e is NotPositiveAmountException || e is InsufficientBalanceException)
        {
            return BadRequest(new { e.Message, Reason = nameof(e) });
        }
    }

    [HttpPut("Deposit")]
    public async Task<ActionResult<BalanceUpdateResponse>> Deposit([FromBody] DepositBody depositBody)
    {
        try
        {
            var currentUser = await UserUtils.GetCurrentUser(User, userManager);
            var balanceUpdate = _bank.Deposit(currentUser.Id, depositBody.Amount);
            return Ok(BalanceUpdateResponse.FromBalanceUpdate(balanceUpdate));
        }
        catch (NotPositiveAmountException e)
        {
            return BadRequest(new { e.Message });
        }
    }

    [HttpGet("History")]
    public async Task<ActionResult<List<BalanceUpdateResponse>>> History()
    {
        var currentUser = await UserUtils.GetCurrentUser(User, userManager);
        return _bank.GetUserBalanceHistory(currentUser.Id)
                    .OrderByDescending(b => b.At)
                    .Select(BalanceUpdateResponse.FromBalanceUpdate)
                    .ToList();
    }
}
