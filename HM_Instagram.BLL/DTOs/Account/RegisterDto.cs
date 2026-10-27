using HM_Instagram.BLL.Enums;

namespace HM_Instagram.BLL.DTOs.Account;

public class RegisterDto
{
    public string Nickname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Desc { get; set; }
    public Gender? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public UploadFileDto Avatar { get; set; } = new();
}
