namespace HomeWork_61_Instagram.Models;

public class Comments
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    
    
    
    
    public Post? Post { get; set; }
    public MyUser? User { get; set; }
}