
using Dal.Models;

namespace WebAPI.Models
{
    public class CreateUserBody
    {
        public required string DisplayName { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required UserType UserType { get; init; }
    }
}
