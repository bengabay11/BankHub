using Dal.Models;

namespace WebAPI.Models
{
    public class BalanceUpdateResponse
    {
        public required Guid Id { get; set; }
        public required BalanceActionType Action { get; set; }
        public required decimal Amount { get; init; }
        public required DateTime At { get; init; }
        public required decimal BalanceAfter { get; init; }

        public static BalanceUpdateResponse FromBalanceUpdate(BalanceUpdate balanceUpdate)
        {
            return new BalanceUpdateResponse
            {
                Id = balanceUpdate.Id,
                Action = balanceUpdate.Action,
                Amount = balanceUpdate.Amount,
                At = balanceUpdate.At,
                BalanceAfter = balanceUpdate.BalanceAfter
            };
        }
    }
}
