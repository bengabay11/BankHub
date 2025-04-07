using Microsoft.AspNetCore.Identity;

namespace Dal.Models
{
    public class User : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
        public decimal Balance { get; set; } = 0;
        public UserType Type { get; set; }

        // Navigation properties
        public ICollection<Transfer> IncomingTransfers { get; set; } = [];
        public ICollection<Transfer> OutgoingTransfers { get; set; } = [];
    }
}
