using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.IdentityContext;
public class IdentityContextSeed
{
    public static async Task SeedAsync(IdentityContext identityDbContext, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole<long>(RoleName.Customer));
        await roleManager.CreateAsync(new IdentityRole<long>(RoleName.Administrator));

        User defaultUser = new User
        {
            UserName = "user@gmail.com",
            Email = "user@gmail.com",
        };

        await userManager.CreateAsync(defaultUser, "P@ssword1");
        User? createdUser = await userManager.FindByEmailAsync("user@gmail.com");
        if (createdUser != null)
        {
            await userManager.AddToRoleAsync(defaultUser, RoleName.Customer);
        }


        User admin = new User
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com"
        };
        await userManager.CreateAsync(admin, "P@ssword1");
        User? createdAdmin = await userManager.FindByEmailAsync("admin@gmail.com");
        if (createdAdmin != null)
        {
            await userManager.AddToRoleAsync(createdAdmin, RoleName.Administrator);
        }
    }
}
