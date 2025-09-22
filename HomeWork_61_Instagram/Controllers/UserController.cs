using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    // GET
    public IActionResult Index()
    {
        var userid = int.Parse(_userManager.GetUserId(User)); 
        var users = _userManager.Users.Include(p => p.Posts)
            .Include(p => p.Followers)
            .Include(p => p.Following)
            .FirstOrDefault(u => u.Id == userid);
        return View(users);
    }
    [Authorize]
    public async Task<IActionResult> SearchUsers(string? searchTitle)
    {
        var users = _userManager.Users.Include(p => p.Posts)
            .Include(p => p.Followers)
            .Include(p => p.Following).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchTitle))
        { 
            users = users.Where(u => u.UserName.ToLower().Contains(searchTitle.ToLower())
            || u.Email.ToLower().Contains(searchTitle.ToLower())
            || u.Name.ToLower().Contains(searchTitle.ToLower())
            || u.Desc.ToLower().Contains(searchTitle.ToLower()));
        }
         return View(await users.ToListAsync());
    }
    [Authorize]
    public IActionResult Profile(int id)
    {
        if (!(id == int.Parse(_userManager.GetUserId(User))))
        {
            var user = _userManager.Users
                .Include(p => p.Posts)
                .Include(p => p.Followers)
                .Include(p => p.Following)
                .FirstOrDefault(u => u.Id == id);
            return View(user);
        }
        return RedirectToAction("Index");
    }

    public IActionResult DetailInfo(int PostId)
    {
        if (PostId == null)
            return NotFound();
        var post = _context.Posts
            .Include(p => p.Reactions)
            .Include(u => u.User)
            .Include(u => u.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefault(p => p.Id == PostId);
        if (post == null)
            return NotFound();
        var currentUserId = int.Parse(_userManager.GetUserId(User));

        if (post != null && post.Reactions != null)
        {
            ViewBag.IsLiked = post.Reactions.Any(r => r.UserId == currentUserId);
        }
        else
        {
            ViewBag.IsLiked = false;
        }
        return View(post);
    }
}