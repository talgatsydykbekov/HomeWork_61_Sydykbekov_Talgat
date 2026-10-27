namespace HM_Instagram.BLL.DTOs.Post;

public class CreatePostDto
{
    public int UserId { get; set; }
    public string? Desc { get; set; }
    public UploadFileDto Image { get; set; } = new();
}
