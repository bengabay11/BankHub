using Dal.Models;

namespace WebAPI.Models
{
    public class ExtendedUserResponse
    {
        public required Guid Id { get; set; }
        public required string DisplayName { get; set; }
        public UserType Type { get; set; }
        public required string? Email { get; set; }
        public required string? UserName { get; set; }
        public required decimal Balance { get; set; }
        public required Permission[] Permissions { get; set; }

        public static ExtendedUserResponse FromUser(User user, Permission[] permissions)
        {
            return new ExtendedUserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Type = user.Type,
                Balance = user.Balance,
                Email = user.Email,
                UserName = user.UserName,
                Permissions = permissions
            };
        }
    }
}
