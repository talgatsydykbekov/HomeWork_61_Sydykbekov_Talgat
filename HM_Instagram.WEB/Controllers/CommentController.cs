using System.Security.Claims;
using HM_Instagram.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM_Instagram.WEB.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    [Authorize]

    public async Task<IActionResult> Index(int PostId)
    {
        var comments = await _commentService.GetCommentsAsync(PostId);
        if (comments == null)
        {
            return NotFound();
        }
        return View(comments);
    }
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(CacheProfileName = "NoCache")]
    public async  Task<IActionResult> AddComment(int postId, string commentText)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction(
                "Index",
                "Comment",
                new { PostId = postId });
        }
        if (string.IsNullOrWhiteSpace(commentText))
        {
            ModelState.AddModelError(
                nameof(commentText),
                "Комментарий не может быть пустым.");

            return RedirectToAction(
                "Index",
                "Comment",
                new { PostId = postId });
        }
        var created = await _commentService.AddComment(
            postId,
            commentText,
            userId);
        if (!created)
        {
            TempData["ErrorMessage"] = "Комментарий не удалось добавить.";
        }
        return RedirectToAction("DetailInfo", "User", new { PostId = postId });
    }
}