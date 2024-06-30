using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealTimeTaskManagement.Data.Context;
using RealTimeTaskManagement.Data.Entities;

namespace RealTimeTaskManagement.Data;
public static class RealTimeTaskManagementSeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<RealTimeTaskManagementDbContext>();
            if (!roleManager!.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            if (!userManager!.Users.Any())
            {
                var adminUser = new UserEntity
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Manager" });

                var normalUser = new UserEntity
                {
                    UserName = "user@user.com",
                    Email = "user@user.com",
                    FirstName = "Normal",
                    LastName = "User",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(normalUser, "User@123");
                await userManager.AddToRoleAsync(normalUser, "User");
            }

            await context.SaveChangesAsync();
        }
    }
}
