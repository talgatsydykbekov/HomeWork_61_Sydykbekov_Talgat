using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_61_Instagram.Controllers;

public class HomeController : Controller
{
    private MyUserContext _context;
    private readonly UserManager<MyUser> _userManager;
    private ILogger<HomeController> _logger;
    public HomeController(UserManager<MyUser> userManager, MyUserContext context,ILogger<HomeController> logger)
    {
        _userManager = userManager;
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var user = _userManager.GetUserId(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }
        var someuser = _userManager.Users.Include(u => u.Following).FirstOrDefault(u => u.Id == int.Parse(user));
        var followingIds = someuser.Following.Select(f => f.FollowedId).ToList();
        
        var  posts = _context.Posts.Include(u => u.User).Include(p => p.Reactions)
            .Where(u => followingIds.Contains(u.UserId));
        

        return View(posts.ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}