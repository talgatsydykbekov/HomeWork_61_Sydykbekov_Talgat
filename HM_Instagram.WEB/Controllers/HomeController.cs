using System.Diagnostics;
using System.Security.Claims;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using HM_Instagram.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM_Instagram.WEB.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;

    public HomeController(IPostService postService)
    {
        _postService = postService;
    }
    [ResponseCache(CacheProfileName = "NoCache")]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(!int.TryParse(userId, out var currentUserId))
        {
            return RedirectToAction("Login", "Account");
        }

        var posts = await _postService.GetFeedAsync(currentUserId);
        var model = posts.Select(post => new GetPostViewModel
        {
            Content = post.Content,
            Ceated_Datetime = post.Ceated_Datetime,
            Comments = post.Comments,
            Reactions = post.Reactions,
            UserId = post.UserId,
            User = post.User,
            Id = post.Id,
        }).ToList();
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}