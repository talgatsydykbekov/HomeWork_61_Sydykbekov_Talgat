using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Interfaces;

public interface IUserRepository
{
    Task<MyUser?> GetByIdAsync(int userId);
    Task<List<MyUser>> SearchAsync(string? searchTitle);
    Task<Post?> GetPostDetailsAsync(int postId);
}
