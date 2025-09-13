namespace HomeWork_61_Instagram.Models;

public class UserFollow
{
    public int Id { get; set; }
    public int? FollowerId { get; set; }
    public int? FollowedId { get; set; }
    public MyUser? Follower { get; set; }
    public MyUser? Followed { get; set; }
}