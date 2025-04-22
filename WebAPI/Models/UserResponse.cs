using Dal.Models;

namespace WebAPI.Models
{
    public class UserResponse
    {
        public required Guid Id { get; set; }
        public required string DisplayName { get; set; }
        public UserType Type { get; set; }

        public static UserResponse FromUser(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Type = user.Type
            };
        }
    }
}
