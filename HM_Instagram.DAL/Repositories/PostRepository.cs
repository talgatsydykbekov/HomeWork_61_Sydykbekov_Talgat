using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using HM_Instagram.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HM_Instagram.DAL.Repositories;

public class PostRepository : IPostRepository
{
    private readonly MyUserContext _context;

    public PostRepository(MyUserContext context)
    {
        _context = context;
    }
    public async Task<List<int?>> GetFollowingIdsAsync(int userId)
    {
        return await _context.Users
            .Where(user => user.Id == userId)
            .SelectMany(user => user.Following)
            .Select(follow => follow.FollowedId)
            .ToListAsync();
    }

    public async Task<List<Post>> GetPostsByAuthorsAsync(List<int?> authorIds)
    {
        return await _context.Posts
            .AsNoTracking()
            .Include(post => post.User)
            .Include(post => post.Reactions)
            .Include(post => post.Comments)
            .Where(post => authorIds.Contains(post.UserId))
            .ToListAsync();
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        var model =  _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<bool> FindPostByIdAsync(int postId)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
        if (post == null)
        {
            return false;
        }
        return true;
    }
}