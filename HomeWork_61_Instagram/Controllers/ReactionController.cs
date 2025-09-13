using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HomeWork_61_Instagram.Controllers;

public class ReactionController : Controller
{
    private readonly IWebHostEnvironment _env;
    private MyUserContext _context;
    private readonly UserManager<MyUser> _userManager;

    public ReactionController(UserManager<MyUser> userManager, MyUserContext context, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _context = context;
        _env = env;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddLike(int PostId)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == PostId);
        if (post == null)
            return NotFound();
        var userid = int.Parse(_userManager.GetUserId(User));
        if (userid == null)
            return NotFound();
        var reactions = _context.Reactions.FirstOrDefault(r => r.UserId == userid && r.PostId == PostId);
        if (reactions != null)
        {
            TempData["ErrorMessage"] = "Вы уже лайкали этот пост";
            return RedirectToAction("Index", "Home");
        }
        else
        {
            var reaction = new Reactions()
            {
                PostId = PostId,
                UserId = userid
            };
            TempData["SuccessMessage"] = "";
            _context.Reactions.Add(reaction);
            await _context.SaveChangesAsync();
        }
        
        return RedirectToAction("Index" , "Home",new { id = PostId });
    }
}