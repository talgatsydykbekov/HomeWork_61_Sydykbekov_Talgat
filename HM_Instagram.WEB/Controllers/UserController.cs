using System.Security.Claims;
using HM_Instagram.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM_Instagram.WEB.Controllers;

[ResponseCache(CacheProfileName = "NoCache")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        var user = await _userService.GetProfileAsync(userId, userId);
        if (user == null)
            return NotFound();

        return View(user);
    }

    [Authorize]
    public async Task<IActionResult> SearchUsers(string? searchTitle)
    {
        return View(await _userService.SearchAsync(searchTitle));
    }

    [Authorize]
    public async Task<IActionResult> Profile(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        if (id == userId)
            return RedirectToAction(nameof(Index));

        var user = await _userService.GetProfileAsync(id, userId);
        if (user == null)
            return NotFound();

        return View(user);
    }

    public async Task<IActionResult> DetailInfo(int PostId)
    {
        int? currentUserId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
            ? userId
            : null;

        var post = await _userService.GetPostDetailsAsync(PostId, currentUserId);
        if (post == null)
            return NotFound();

        return View(post);
    }
}
