using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Interfaces;

public interface IPostRepository
{
    Task<List<int?>> GetFollowingIdsAsync(int userId);

    Task<List<Post>> GetPostsByAuthorsAsync(List<int?> authorIds);
    Task<Post> CreatePostAsync(Post post);
    Task<bool> FindPostByIdAsync(int postId);
}