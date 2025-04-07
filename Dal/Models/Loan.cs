namespace Dal.Models
{
    public class Loan
    {
        public required Guid Identifier { get; set; }
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public required decimal Amount { get; set; }
        public required decimal AnnualInterestRate { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime ExpiresAt { get; set; }
    }
}
