using System.Globalization;
using HomeWork_61_Instagram.Models;
using HomeWork_61_Instagram.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add services to the container.
        builder.Services.AddControllersWithViews(options =>
        {
            options.CacheProfiles.Add("Caching", new CacheProfile()
                {
                    Duration = 100,
                    Location = ResponseCacheLocation.Any
                });
            options.CacheProfiles.Add("NoCache", new CacheProfile()
            {
                Location = ResponseCacheLocation.None,
                NoStore = true
            });
        }).AddViewLocalization();
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<MyUserContext>(options => options.UseSqlite(connectionString)).
            AddIdentity<MyUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                
            }). 
            AddEntityFrameworkStores<MyUserContext>();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });
        builder.Services.AddMemoryCache();
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<MyUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
                await AdminInitializer.SeedAdminUser(roleManager, userManager);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        var supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("ru"),
        };
        app.UseRequestLocalization(new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture("en"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseResponseCompression();
        app.MapStaticAssets();

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();
        
        app.Run();
    }
}

