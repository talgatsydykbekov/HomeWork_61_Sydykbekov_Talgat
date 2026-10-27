namespace HM_Instagram.BLL.DTOs.User;

public class UserSummaryDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string Avatar { get; set; } = string.Empty;
    public string? Name { get; set; }
}
