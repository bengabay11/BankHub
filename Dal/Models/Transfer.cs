using System.ComponentModel.DataAnnotations;

namespace Dal.Models
{
    public class Transfer
    {
        public Guid Id { get; set; }

        // Foreign keys
        public required Guid GiverUserId { get; init; }
        public required Guid TakerUserId { get; init; }

        // Navigation properties
        public User? GiverUser { get; set; }
        public User? TakerUser { get; set; }

        public required decimal Amount { get; init; }
        public required DateTime At { get; init; }
    }
}
