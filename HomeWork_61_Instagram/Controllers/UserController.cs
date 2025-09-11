using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_61_Instagram.Controllers;

public class UserController : Controller
{
    private MyUserContext _context;
    private readonly UserManager<MyUser> _userManager;

    public UserController(UserManager<MyUser> userManager, MyUserContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        var userid = int.Parse(_userManager.GetUserId(User));
        var users = _userManager.Users.Include(p => p.Posts)
            .FirstOrDefault(u => u.Id == userid);
        return View(users);
    }
    
}