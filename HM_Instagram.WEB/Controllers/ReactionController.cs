using System.Security.Claims;
using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM_Instagram.WEB.Controllers;

public class ReactionController : Controller
{
    private readonly IReactionService _reactionService;

    public ReactionController(IReactionService reactionService)
    {
        _reactionService = reactionService;
    }

    public IActionResult Index() => View();

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddLike(int PostId)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        var result = await _reactionService.AddLikeAsync(new ReactionsDto
        {
            UserId = userId,
            PostId = PostId
        });

        if (result == null)
            return NotFound();

        return Json(result);
    }
}
