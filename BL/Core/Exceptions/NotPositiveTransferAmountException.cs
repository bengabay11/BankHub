namespace BL.Core.Exceptions;

public class NotPositiveAmountException(decimal amount, string action)
: Exception($"Amount for {action} must be positive. Amount Given: '{amount}'")
{
    public decimal Amount { get; } = amount;
    public string Action { get; } = action;
}
