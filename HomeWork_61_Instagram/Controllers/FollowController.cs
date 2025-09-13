using HomeWork_61_Instagram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_61_Instagram.Controllers;

public class FollowController : Controller
{
    private MyUserContext _context;
    private readonly UserManager<MyUser> _userManager;

    public FollowController(UserManager<MyUser> userManager, MyUserContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Followed(int followedId)
    {
        var userid = int.Parse(_userManager.GetUserId(User));
        
        bool alreadyFollow = await _context.UserFollows.AnyAsync(f => f.FollowerId == userid && f.FollowedId == followedId);
        if (!alreadyFollow)
        {
            var follow = new UserFollow()
            {
                FollowedId = followedId,
                FollowerId = userid,
            };
            _context.UserFollows.Add(follow);
            await  _context.SaveChangesAsync();
        }
        return RedirectToAction("Profile", "User" ,new {id = followedId});
    }
    [Authorize]
    // подписчики
    public async Task<IActionResult> Followers(int Id)
    {
        var followers = await _context.UserFollows
            .Where(f => f.FollowedId == Id)
            .Include(f => f.Follower)
            .ToListAsync();
        return View(followers);
    }
    //подписки
    [Authorize]
    public async Task<IActionResult> Following(int Id)
    {
        var following = await _context.UserFollows
            .Where(f => f.FollowerId == Id)
            .Include(f => f.Followed)
            .ToListAsync();
        return View(following);
    }
        
}