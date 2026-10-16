using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace HomeWork_61_Instagram.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    [Display(Name = "RememberMe")]
    public bool RememberMe { get; set; }
    
    
    public string? ReturnUrl { get; set; }
}