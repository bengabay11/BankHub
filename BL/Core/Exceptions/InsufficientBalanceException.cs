namespace BL.Core.Exceptions
{
    public class InsufficientBalanceException(
        decimal currentBalance,
        decimal attemptedWithdrawal,
        decimal minimumRequiredBalance
    ) : Exception($"Insufficient balance. Current: {currentBalance}, Attempted withdrawal: {attemptedWithdrawal}, Minimum required balance: {minimumRequiredBalance}")
    {
        public decimal CurrentBalance { get; } = currentBalance;
        public decimal AttemptedWithdrawal { get; } = attemptedWithdrawal;
        public decimal MinimumRequiredBalance { get; } = minimumRequiredBalance;
    }
}
