using Dal.Models;

namespace WebAPI.Models
{
    public class UserResponse : OtherUserResponse
    {
        public required string? Email;
        public required decimal Balance;

        public static new UserResponse FromUser(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Type = user.Type,
                Balance = user.Balance,
                Email = user.Email
            };
        }
    }
}
