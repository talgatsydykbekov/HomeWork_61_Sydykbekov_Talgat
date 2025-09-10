
using HomeWork_61_Instagram.Models;
using HomeWork_61_Instagram.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HomeWork_60_Identity.Controllers;

public class AccountController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly UserManager<MyUser> _userManager;
    private readonly SignInManager<MyUser> _signInManager;

    public AccountController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _env = env;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            string? relativePath = null;
            string uploadedFileName = Path.Combine(_env.WebRootPath, "images/avatars");
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
            relativePath = Path.Combine("images/avatars", fileName.Replace("\\", "/"));
            MyUser user = new MyUser
            {
               UserName = model.Nickname,
                Name = model.Name,
                Email = model.Email,
                Avatar = relativePath,
                Desc = model.Desc,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel {ReturnUrl = returnUrl});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            MyUser user = await _userManager.FindByEmailAsync(model.EmailOrLogin);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.EmailOrLogin);
            }
            if (user == null)
            {
                ModelState.AddModelError("","Пользователь не найден");
                return View(model);
            }
            
            SignInResult result = await _signInManager.PasswordSignInAsync(
                user, model.Password, 
                model.RememberMe,
                false
                );
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Неправильный логин или пароль");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}