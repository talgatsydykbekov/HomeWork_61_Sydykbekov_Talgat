using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_61_Instagram.Controllers;

public class CommentController : Controller
{
    private MyUserContext _context;
    private readonly UserManager<MyUser> _userManager;

    public CommentController(UserManager<MyUser> userManager, MyUserContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    [Authorize]
    public IActionResult Index(int PostId)
    {
        var comments = _context.Comments.Include(u => u.User).Where(u => u.PostId == PostId);
        if (comments == null)
        {
            return NotFound();
        }
        return View(comments.ToList());
    }
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddComment(int PostId, string commentText)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == PostId);
        if (post == null)
        {
            return NotFound();
        }
        var userid = int.Parse(_userManager.GetUserId(User));
        if (ModelState.IsValid && !string.IsNullOrEmpty(commentText))
        {
            var comment = new Comments()
            {
                Comment = commentText,
                PostId = PostId,
                UserId = userid,
                Created_DateTime = DateTime.UtcNow
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new { id = userid });
        }
        return RedirectToAction("Index", "Home", new { id = userid });
    }
}