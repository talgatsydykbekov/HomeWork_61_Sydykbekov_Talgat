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
        
        // if (!string.IsNullOrEmpty(searchTitle))
        // {
        //   users = _userManager.Users.Where(u => u.UserName == searchTitle
        //                                   || u.Email == searchTitle
        //                                   || u.Desc == searchTitle
        //                                   || u.Name == searchTitle);
        // }
        var userid = int.Parse(_userManager.GetUserId(User)); 
        var users = _userManager.Users.Include(p => p.Posts)
            .FirstOrDefault(u => u.Id == userid);
        return View(users);
    }

    public async Task<IActionResult> SearchUsers(string? searchTitle)
    {
        var users = _userManager.Users.Include(p => p.Posts).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchTitle))
        { 
            users = users.Where(u => u.UserName.ToLower().Contains(searchTitle.ToLower())
            || u.Email.ToLower().Contains(searchTitle.ToLower())
            || u.Name.ToLower().Contains(searchTitle.ToLower())
            || u.Desc.ToLower().Contains(searchTitle.ToLower()));
        }
         return View(await users.ToListAsync());
    }

    public IActionResult Profile(int id)
    {
        if (!(id == int.Parse(_userManager.GetUserId(User))))
        {
            var user = _userManager.Users.Include(p => p.Posts)
                .FirstOrDefault(u => u.Id == id);
            return View(user);
        }
        return RedirectToAction("Index");
    }
}