using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.DTOs.User;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileDto?> GetProfileAsync(int userId, int currentUserId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return null;

        return new UserProfileDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Avatar = user.Avatar,
            Name = user.Name,
            Desc = user.Desc,
            FollowersCount = user.Followers?.Count ?? 0,
            FollowingCount = user.Following?.Count ?? 0,
            IsFollowing = user.Followers?.Any(f => f.FollowerId == currentUserId) ?? false,
            Posts = user.Posts?.Select(p => new UserPostDto
            {
                Id = p.Id,
                Content = p.Content
            }).ToList() ?? new()
        };
    }

    public async Task<List<UserSummaryDto>> SearchAsync(string? searchTitle)
    {
        var users = await _userRepository.SearchAsync(searchTitle);
        return users.Select(ToSummary).ToList();
    }

    public async Task<PostDetailsDto?> GetPostDetailsAsync(int postId, int? currentUserId)
    {
        var post = await _userRepository.GetPostDetailsAsync(postId);
        if (post == null)
            return null;

        return new PostDetailsDto
        {
            Id = post.Id,
            Content = post.Content,
            Desc = post.Desc,
            Created = post.Ceated_Datetime,
            User = post.User == null ? null : ToSummary(post.User),
            IsLiked = currentUserId.HasValue &&
                      (post.Reactions?.Any(r => r.UserId == currentUserId.Value) ?? false),
            LikesCount = post.Reactions?.Count ?? 0,
            Comments = post.Comments?.Select(c => new GetCommentDto
            {
                Avatar = c.User?.Avatar ?? string.Empty,
                UserName = c.User?.UserName ?? string.Empty,
                Content = c.Comment,
                Created = c.Created_DateTime
            }).ToList() ?? new()
        };
    }

    private static UserSummaryDto ToSummary(MyUser user) => new()
    {
        Id = user.Id,
        UserName = user.UserName,
        Avatar = user.Avatar,
        Name = user.Name
    };
}
