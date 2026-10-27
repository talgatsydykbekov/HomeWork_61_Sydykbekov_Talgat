using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Interfaces;

public interface IFollowRepository
{
    Task<bool> FindAsync(int followerId, int followedId);
    Task AddAsync(int followerId, int followedId);
    Task SaveChangesAsync();
    Task<int> CountFollowersAsync(int followedId);
    Task<bool> RemoweAsync(int followerId, int followedId);
    Task<List<UserFollow>> GetFollowingAsync(int followerId);
    Task<List<UserFollow>> GetFollowersAsync(int followedId);
}