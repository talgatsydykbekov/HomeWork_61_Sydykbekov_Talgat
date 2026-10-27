using System.Security.Claims;
using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.DTOs.Post;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM_Instagram.WEB.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    public IActionResult Index() => View();

    [Authorize]
    public IActionResult Create() => View();

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostViewModel post, IFormFile? file)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        if (file == null || file.Length == 0)
            ModelState.AddModelError(string.Empty, "Выберите изображение для публикации.");

        if (!ModelState.IsValid)
            return View(post);

        await using var stream = file!.OpenReadStream();
        await _postService.CreatePost(new CreatePostDto
        {
            UserId = userId,
            Desc = post.Desc,
            Image = new UploadFileDto { FileName = file.FileName, Content = stream }
        });
        return RedirectToAction("Index", "User");
    }
}
