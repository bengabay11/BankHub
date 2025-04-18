using Dal.Models;

namespace WebAPI.Models
{
    public class OtherUserResponse
    {
        public required Guid Id { get; set; }
        public required string DisplayName { get; set; }
        public UserType Type { get; set; }

        public static OtherUserResponse FromUser(User user)
        {
            return new OtherUserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Type = user.Type
            };
        }
    }
}
