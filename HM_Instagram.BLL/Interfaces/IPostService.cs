using HM_Instagram.BLL.DTOs.Post;
using HM_Instagram.BLL.Models;


namespace HM_Instagram.BLL.Interfaces;

public interface IPostService
{
    Task<List<Post>> GetFeedAsync(int currentUserId);
    Task<Post> CreatePost(CreatePostDto post);
}