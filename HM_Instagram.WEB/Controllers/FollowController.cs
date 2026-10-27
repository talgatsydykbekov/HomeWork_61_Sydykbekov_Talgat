using System.Security.Claims;
using HM_Instagram.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM_Instagram.WEB.Controllers;

public class FollowController : Controller
{
    private readonly IFollowService _followService;

    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }
    [Authorize]
    [HttpPost]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Followed(int followedId)
    {
        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userid, out var userId))
        {
            return Unauthorized();
        }
        var result = await _followService.FollowAsync(userId,followedId);
        return Json(new
        {
            isFollowing = result.IsFollowing,
            followersCount = result.FollowersCount
        });
    }
    [Authorize]
    [HttpPost]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> UnFollowed(int followedId)
    {
        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(!int.TryParse(userid, out var userId))
        {
            return Unauthorized();
        }
        var result = await _followService.UnfollowAsync(userId, followedId);
        return Json(new
        {
            isFollowing = result.IsFollowing,
            followersCount = result.FollowersCount
        });
    }
    [Authorize]
    [ResponseCache(CacheProfileName = "Caching")]
    // подписчики
    public async Task<IActionResult> Followers(int id)
    {
        var followers = await _followService.GetFollowersAsync(id);
        return View(followers);
    }
    //подписки
    [Authorize]
    [ResponseCache(CacheProfileName = "Caching")]
    public async Task<IActionResult> Following(int Id)
    {
        var following = await _followService.GetFollowingAsync(Id);

        return View(following);
    }
    public async Task<IActionResult> GetFollowersCount(int id)
    {
        if (id != null)
        {
            var following = await _followService.GetFollowersAsync(id);

            return View(following);
        }
        return NotFound();
    }
}