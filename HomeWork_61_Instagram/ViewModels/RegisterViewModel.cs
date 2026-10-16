using System.ComponentModel.DataAnnotations;
using HomeWork_61_Instagram.Enums;

namespace HomeWork_61_Instagram.ViewModels;

public class RegisterViewModel
{
    [Required] public string Nickname { get; set; }
    [Required] public string Email { get; set; }


    public string? Name { get; set; }
    public string? Desc { get; set; }

    public Gender? Gender { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    public string PasswordConfirm { get; set; }

    [Phone] [Display(Name = "NumberTel")] public string? PhoneNumber { get; set; }
}