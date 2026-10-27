using HM_Instagram.BLL.DTOs.Account;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace HM_Instagram.BLL.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<MyUser> _userManager;
    private readonly SignInManager<MyUser> _signInManager;
    private readonly IImageStorage _imageStorage;

    public AccountService(UserManager<MyUser> userManager,
        SignInManager<MyUser> signInManager, IImageStorage imageStorage)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _imageStorage = imageStorage;
    }

    public async Task<AccountResultDto> RegisterAsync(RegisterDto registration)
    {
        var avatar = await _imageStorage.SaveAsync(registration.Avatar, "avatars");
        var user = new MyUser
        {
            UserName = registration.Nickname,
            Email = registration.Email,
            Name = registration.Name,
            Desc = registration.Desc,
            Gender = registration.Gender,
            PhoneNumber = registration.PhoneNumber,
            Avatar = avatar
        };

        var created = false;
        try
        {
            var result = await _userManager.CreateAsync(user, registration.Password);
            if (!result.Succeeded)
                return FromIdentity(result);

            created = true;
            var roleResult = await _userManager.AddToRoleAsync(user, "user");
            if (!roleResult.Succeeded)
            {
                var deleted = await _userManager.DeleteAsync(user);
                created = !deleted.Succeeded;
                return new AccountResultDto
                {
                    Errors = roleResult.Errors.Concat(deleted.Errors)
                        .Select(e => e.Description).ToList()
                };
            }

            await _signInManager.SignInAsync(user, false);
            return new AccountResultDto { Succeeded = true };
        }
        finally
        {
            if (!created)
                await _imageStorage.DeleteAsync(avatar);
        }
    }

    public async Task<AccountResultDto> LoginAsync(LoginDto login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email)
                   ?? await _userManager.FindByNameAsync(login.Email);
        if (user == null)
            return new AccountResultDto { Errors = new() { "Пользователь не найден" } };

        var result = await _signInManager.PasswordSignInAsync(
            user, login.Password, login.RememberMe, false);
        return new AccountResultDto
        {
            Succeeded = result.Succeeded,
            Errors = result.Succeeded ? new() : new() { "Неправильный логин или пароль" }
        };
    }

    public Task LogOffAsync() => _signInManager.SignOutAsync();

    private static AccountResultDto FromIdentity(IdentityResult result) => new()
    {
        Succeeded = result.Succeeded,
        Errors = result.Errors.Select(e => e.Description).ToList()
    };
}
