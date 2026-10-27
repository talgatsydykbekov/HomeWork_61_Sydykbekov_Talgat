namespace HM_Instagram.BLL.DTOs.Account;

public class AccountResultDto
{
    public bool Succeeded { get; set; }
    public List<string> Errors { get; set; } = new();
}
