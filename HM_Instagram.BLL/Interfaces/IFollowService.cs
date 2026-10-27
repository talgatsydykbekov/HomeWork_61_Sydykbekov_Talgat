using HM_Instagram.BLL.DTOs.Follow;
using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Interfaces;

public interface IFollowService
{
    Task<FollowResultDto> FollowAsync(int followerId, int followedId);
    Task<FollowResultDto> UnfollowAsync(int followerId, int followedId);
    Task<List<FollowDto>> GetFollowingAsync(int followerId);
    Task<List<FollowDto>> GetFollowersAsync(int followerId);
}