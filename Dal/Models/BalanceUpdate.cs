namespace Dal.Models
{
    public class BalanceUpdate
    {
        public required Guid Id { get; set; }
        public required BalanceActionType Action { get; set; }
        public required decimal Amount { get; init; }
        public required DateTime At { get; init; }
        public required decimal BalanceAfter { get; init; }

        // Foreign keys
        public required Guid UserId { get; init; }

        // Navigation properties
        public User? User { get; set; }
    }
}
