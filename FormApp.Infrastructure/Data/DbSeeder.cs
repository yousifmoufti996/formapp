using FormApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FormApp.Infrastructure.Data.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext dbContext)
    {
        // Ensure database is created
        await dbContext.Database.MigrateAsync();

        // Seed roles
        await SeedRoles(roleManager);

        // Seed super admin user
        await SeedSuperAdmin(userManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roleNames = { "SuperAdmin", "User" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName });
                Console.WriteLine($"✓ Role '{roleName}' created successfully");
            }
        }
    }

    private static async Task SeedSuperAdmin(UserManager<User> userManager)
    {
        var superAdminEmail = "superadmin@soc.iq";
        var superAdminUsername = "admin";
        var superAdminPassword = "SuperAdmin@123";

        // Check if super admin already exists
        var existingAdmin = await userManager.FindByEmailAsync(superAdminEmail);

        if (existingAdmin == null)
        {
            var superAdmin = new User
            {
                UserName = superAdminUsername,
                Email = superAdminEmail,
                EmailConfirmed = true,
                FirstName = "Super",
                LastName = "Admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(superAdmin, superAdminPassword);
            
            if (result.Succeeded)
            {
                // Assign SuperAdmin role
                await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                
                Console.WriteLine($"✓ Super Admin created successfully");
                Console.WriteLine($"  Username: {superAdminUsername}");
                Console.WriteLine($"  Email: {superAdminEmail}");
                Console.WriteLine($"  Password: {superAdminPassword}");
            }
            else
            {
                Console.WriteLine($"✗ Failed to create Super Admin:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"  - {error.Description}");
                }
            }
        }
    }
}
