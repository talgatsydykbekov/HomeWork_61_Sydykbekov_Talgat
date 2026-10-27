using HM_Instagram.BLL.DTOs.Post;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IImageStorage _imageStorage;

    public PostService(IPostRepository postRepository, IImageStorage imageStorage)
    {
        _postRepository = postRepository;
        _imageStorage = imageStorage;
    }

    public async Task<List<Post>> GetFeedAsync(int currentUserId)
    {
        var followingIds = await _postRepository.GetFollowingIdsAsync(currentUserId);
        return await _postRepository.GetPostsByAuthorsAsync(followingIds);
    }

    public async Task<Post> CreatePost(CreatePostDto post)
    {
        var content = await _imageStorage.SaveAsync(post.Image, "posts");
        var newPost = new Post
        {
            Content = content,
            Ceated_Datetime = DateTime.UtcNow,
            UserId = post.UserId,
            Desc = post.Desc
        };
        try
        {
            await _postRepository.CreatePostAsync(newPost);
        }
        catch
        {
            await _imageStorage.DeleteAsync(content);
            throw;
        }
        return newPost;
    }
}
