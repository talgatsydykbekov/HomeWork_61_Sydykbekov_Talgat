namespace HM_Instagram.BLL.DTOs.User;

public class UserProfileDto : UserSummaryDto
{
    public string? Desc { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    public bool IsFollowing { get; set; }
    public List<UserPostDto> Posts { get; set; } = new();
}
