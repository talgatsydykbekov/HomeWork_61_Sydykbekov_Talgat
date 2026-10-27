using HM_Instagram.BLL.DTOs.User;

namespace HM_Instagram.BLL.Interfaces;

public interface IUserService
{
    Task<UserProfileDto?> GetProfileAsync(int userId, int currentUserId);
    Task<List<UserSummaryDto>> SearchAsync(string? searchTitle);
    Task<PostDetailsDto?> GetPostDetailsAsync(int postId, int? currentUserId);
}
