using HM_Instagram.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace HM_Instagram.BLL.Services;

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