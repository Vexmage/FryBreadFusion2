using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FrybreadFusion.Data
{
    // Dummy class for logging purposes
    public class SeedDataLogger { }

    public class SeedData
    {
        // This method will create the default roles and the default admin user
        public static async Task Initialize(IServiceProvider serviceProvider, ILogger<SeedData> logger)

        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await EnsureRolesAsync(roleManager, logger);
            await EnsureAdminUserAsync(userManager, roleManager, logger);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            var adminRoleName = "Admin";
            var roleExists = await roleManager.RoleExistsAsync(adminRoleName);
            if (!roleExists)
            {
                logger.LogInformation($"Creating the {adminRoleName} role");
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            }
        }

        private static async Task EnsureAdminUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            string adminEmail = "admin@example.com";
            string adminPassword = "AdminPassword123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                logger.LogInformation($"Creating the admin user");
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var userResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    logger.LogWarning("Failed to create the admin user");
                }
            }
        }
    }
}
