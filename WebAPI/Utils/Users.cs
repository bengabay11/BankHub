using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Dal.Models;

namespace WebAPI.Utils
{
    public class UserUtils
    {
        public static async Task<User> GetCurrentUser(ClaimsPrincipal userClaims, UserManager<User> userManager)
        {
            var userId = (userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new UnauthorizedAccessException("User is not authorized.");
            return await userManager.FindByIdAsync(userId) ?? throw new UnauthorizedAccessException("User not found.");
        }
    }
}
