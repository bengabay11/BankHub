using Dal.Models;
using Microsoft.AspNetCore.Identity;

namespace WebAPI
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var configuration = services.GetRequiredService<IConfiguration>();

            string adminRoleName = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(adminRoleName));
            }

            string defaultAdminEmail = configuration["DefaultAdmin:Email"];
            var user = await userManager.FindByEmailAsync(defaultAdminEmail);
            if (user == null)
            {
                // Create the admin user if it doesn't exist
                user = new User
                {
                    UserName = defaultAdminEmail,
                    Email = defaultAdminEmail,
                    DisplayName = configuration["DefaultAdmin:DisplayName"],
                    Type = UserType.Personal
                };

                var result = await userManager.CreateAsync(user, configuration["DefaultAdmin:Password"]);
                if (result.Succeeded)
                {
                    // Add the user to the Admin role
                    await userManager.AddToRoleAsync(user, adminRoleName);
                }
            }
        }
    }
}
