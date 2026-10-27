namespace HM_Instagram.BLL.Models;

public class Comments
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public DateTime Created_DateTime { get; set; }
    
    
    
    public Post? Post { get; set; }
    public MyUser? User { get; set; }
}