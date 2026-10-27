using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using HM_Instagram.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HM_Instagram.DAL.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly MyUserContext _context;

    public FollowRepository(MyUserContext context)
    {
        _context = context;
    }
    
    public Task<bool> FindAsync(int followerId, int followedId)
    {
        return _context.UserFollows.AnyAsync(f =>
            f.FollowerId == followerId &&
            f.FollowedId == followedId);
    }

    public async Task AddAsync(int followerId, int followedId)
    {
        await _context.UserFollows.AddAsync(new UserFollow
        {
            FollowerId = followerId,
            FollowedId = followedId
        });
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public Task<int> CountFollowersAsync(int followedId)
    {
        return _context.UserFollows.CountAsync(f => f.FollowedId == followedId);
    }

    public async Task<bool> RemoweAsync(int followerId, int followedId)
    {
        var follow = _context.UserFollows.FirstOrDefault(f => f.FollowedId == followedId
        && f.FollowerId == followerId);

        if (follow == null)
        {
            return false;
        }
        _context.UserFollows.Remove(follow);
         await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserFollow>> GetFollowingAsync(int followerId)
    {
        return await _context.UserFollows
            .Where(f => f.FollowerId == followerId)
            .Include(f => f.Followed)
            .ToListAsync();
    }

    public async Task<List<UserFollow>> GetFollowersAsync(int followedId)
    {
        return await  _context.UserFollows
            .Where(f => f.FollowedId == followedId)
            .Include(f => f.Follower)
            .ToListAsync();
    }
}