using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IActionResult> AddLike(int PostId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == PostId);
        if (post == null) return NotFound();

        var userId = int.Parse(_userManager.GetUserId(User));

        var reaction = await _context.Reactions
            .FirstOrDefaultAsync(r => r.UserId == userId && r.PostId == PostId);

        if (reaction != null)
        {
            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();

            var likesCount = await _context.Reactions.CountAsync(r => r.PostId == PostId);
            return Json(new { isLiked = false, likesCount });
        }
        else
        {
            var newReaction = new Reactions
            {
                PostId = PostId,
                UserId = userId
            };
            _context.Reactions.Add(newReaction);
            await _context.SaveChangesAsync();

            var likesCount = await _context.Reactions.CountAsync(r => r.PostId == PostId);
            return Json(new { isLiked = true, likesCount });
        }
    }


    

}