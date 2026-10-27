using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.DTOs.Account;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HM_Instagram.WEB.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, IFormFile? file)
    {
        if (file == null || file.Length == 0)
            ModelState.AddModelError(string.Empty, "Выберите файл для профиля");

        if (!ModelState.IsValid)
            return View(model);

        await using var stream = file!.OpenReadStream();
        var result = await _accountService.RegisterAsync(new RegisterDto
        {
            Nickname = model.Nickname,
            Email = model.Email,
            Password = model.Password,
            Name = model.Name,
            Desc = model.Desc,
            Gender = model.Gender,
            PhoneNumber = model.PhoneNumber,
            Avatar = new UploadFileDto { FileName = file.FileName, Content = stream }
        });

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error);
        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null) =>
        View(new LoginViewModel { ReturnUrl = returnUrl });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _accountService.LoginAsync(new LoginDto
        {
            Email = model.Email,
            Password = model.Password,
            RememberMe = model.RememberMe
        });
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return LocalRedirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _accountService.LogOffAsync();
        return RedirectToAction("Index", "Home");
    }
}
