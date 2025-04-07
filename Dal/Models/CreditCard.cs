namespace Dal.Models
{
    public class CreditCard
    {
        public required Guid Identifier { get; set; }
        public required Guid UserId { get; set; }
        public required string Number { get; set; }
        public IEnumerable<Transfer> transfers = [];
        public string? Notes { get; set; }
    }
}
