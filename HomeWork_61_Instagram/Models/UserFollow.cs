namespace HomeWork_61_Instagram.Models;

public class UserFollow
{
    public int Id { get; set; }
    public int? FollowingId { get; set; }
    public int? FollowedId { get; set; }
    public MyUser? Following { get; set; }
    public MyUser? Followed { get; set; }
}