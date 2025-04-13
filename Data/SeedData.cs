using Microsoft.AspNetCore.Identity;
using SeeFrontendTry002.Models;

namespace SeeFrontendTry002.Data;

public abstract class SeedData
{
    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Roles
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(UserRoles.Administrator))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator));
        if (!await roleManager.RoleExistsAsync(UserRoles.ProjectManager))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectManager));
        if (!await roleManager.RoleExistsAsync(UserRoles.PreSalesEngineer))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.PreSalesEngineer));

        // Users
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        // Admin
        string adminUserEmail = "admin1@see.com";
        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
        if (adminUser == null)
        {
            var newAdminUser = new AppUser
            {
                UserName = adminUserEmail,
                Email = adminUserEmail,
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "001",
            };
            await userManager.CreateAsync(newAdminUser, "Test@123");
            await userManager.AddToRoleAsync(newAdminUser, UserRoles.Administrator);
        }

        // Couple
        string pmEmail = "pm1@see.com";
        var pmUser = await userManager.FindByEmailAsync(pmEmail);
        if (pmUser == null)
        {
            pmUser = new AppUser
            {
                UserName = pmEmail,
                Email = pmEmail,
                EmailConfirmed = true,
                FirstName = "Project",
                LastName = "Manager 001"
            };
            await userManager.CreateAsync(pmUser, "Test@123");
            await userManager.AddToRoleAsync(pmUser, UserRoles.ProjectManager);
        }
            

        // Planners
        string[] pseEmails =
        [
            "pse001@see.com",
            "pse002@see.com",
            "pse003@see.com",
            "pse004@see.com"
        ];

        for (int i = 0; i < pseEmails.Length; i++)
        {
            var pseUser = await userManager.FindByEmailAsync(pseEmails[i]);
            if (pseUser == null)
            {
                pseUser = new AppUser
                {
                    UserName = pseEmails[i],
                    Email = pseEmails[i],
                    EmailConfirmed = true,
                    FirstName = $"Pre Sales",
                    LastName = $"Engineer {i + 1}"
                };
                await userManager.CreateAsync(pseUser, "Test@123");
                await userManager.AddToRoleAsync(pseUser, UserRoles.PreSalesEngineer);
            }
                
        }

        await context.SaveChangesAsync();
    }
    
}