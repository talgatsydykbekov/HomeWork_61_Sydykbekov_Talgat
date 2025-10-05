using HomeWork_61_Instagram.Models;
using HomeWork_61_Instagram.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork_61_Instagram.Controllers;

public class PostController : Controller
{
    private readonly IWebHostEnvironment _env;
    private MyUserContext _context;
    private readonly UserManager<MyUser> _userManager;

    public PostController(UserManager<MyUser> userManager, MyUserContext context, IWebHostEnvironment env)
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
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(CacheProfileName = "Caching")]
    public async Task<IActionResult> Create(PostViewModel post, IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            if (ModelState.IsValid)
            {
                string? relativePath = null;
                string uploadedFileName = Path.Combine(_env.WebRootPath, "images/posts");
                if (!Directory.Exists(uploadedFileName))
                {
                    Directory.CreateDirectory(uploadedFileName);
                }

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                string filesavePath = Path.Combine(uploadedFileName, fileName);
                using (var stream = new FileStream(filesavePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                relativePath = $"images/posts/{fileName}";

                var Post = new Post()
                {
                    Content = relativePath,
                    UserId = int.Parse(_userManager.GetUserId(User)),
                    Ceated_Datetime = DateTime.UtcNow,
                    Desc    = post.Desc,
                }; 
                
                await _context.Posts.AddAsync(Post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "User");
            }
        }

        return View(post);
    }
}