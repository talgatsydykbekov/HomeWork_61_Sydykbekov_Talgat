namespace HM_Instagram.BLL.DTOs;

public class GetCommentDto
{
    public string Avatar { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public DateTime Created { get; set; }
}