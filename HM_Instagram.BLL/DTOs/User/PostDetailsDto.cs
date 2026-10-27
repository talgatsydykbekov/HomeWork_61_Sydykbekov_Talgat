namespace HM_Instagram.BLL.DTOs.User;

public class PostDetailsDto : UserPostDto
{
    public string? Desc { get; set; }
    public DateTime Created { get; set; }
    public UserSummaryDto? User { get; set; }
    public bool IsLiked { get; set; }
    public int LikesCount { get; set; }
    public List<GetCommentDto> Comments { get; set; } = new();
}
