using HM_Instagram.BLL.DTOs.Follow;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;

    public FollowService(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }
    
    
    
    public async Task<FollowResultDto> FollowAsync(int followerId, int followedId)
    {
        var alreadyFollowing = await _followRepository
            .FindAsync(followerId, followedId);
        if (!alreadyFollowing)
        {
            await _followRepository.AddAsync(followerId, followedId);
            await _followRepository.SaveChangesAsync();
        }
        
        var followersCount = 
            await _followRepository.CountFollowersAsync(followedId);

        return new FollowResultDto
        {
            IsFollowing = true,
            FollowersCount = followersCount
        };
    }

    public async Task<FollowResultDto> UnfollowAsync(int followerId, int followedId)
    {
        await _followRepository.RemoweAsync(followerId, followedId);
        var followersCount = 
            await _followRepository.CountFollowersAsync(followedId);
        return new FollowResultDto
        {
            IsFollowing = false,
            FollowersCount = followersCount
        };
    }

    public async Task<List<FollowDto>> GetFollowingAsync(int followerId)
    {
        var following =  await _followRepository.GetFollowingAsync(followerId);

        var result = following.Where
                (follow => follow.Followed != null)
            .Select(follow => new FollowDto
            {
                Id = follow.FollowedId,
                UserName = follow.Followed.UserName,
                Avatar = follow.Followed.Avatar
            }).ToList();
        return result;
    }

    public async Task<List<FollowDto>> GetFollowersAsync(int followerId)
    {
        var following =  await _followRepository.GetFollowersAsync(followerId);

        var result = following.Where(follow => follow.Follower != null)
            .Select(follow => new FollowDto
            {
                Id = follow.FollowerId,
                UserName = follow.Follower!.UserName,
                Avatar = follow.Follower.Avatar
            }).ToList();
        return result;
    }
}