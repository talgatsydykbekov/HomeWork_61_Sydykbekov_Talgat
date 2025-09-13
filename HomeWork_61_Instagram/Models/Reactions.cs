namespace HomeWork_61_Instagram.Models;

public class Reactions
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public MyUser? User { get; set; } 
    public Post? Post { get; set; }
}