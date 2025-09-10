using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Identity;

namespace HomeWork_61_Instagram.Services;

public class AdminInitializer
{
    public static async Task SeedAdminUser(RoleManager<IdentityRole<int>> roleManager,
        UserManager<MyUser> userManager)
    {
        var roles = new[] { "user" };

        foreach (var role in roles)
        {
           await roleManager.CreateAsync(new IdentityRole<int>(role)); 
        }
    }
}