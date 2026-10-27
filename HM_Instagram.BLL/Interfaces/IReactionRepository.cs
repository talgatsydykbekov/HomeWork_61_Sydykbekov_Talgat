using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Interfaces;

public interface IReactionRepository
{
    Task<bool> PostExistsAsync(int postId);
    Task<Reactions?> FindAsync(int userId, int postId);
    Task AddAsync(Reactions reaction);
    void Remove(Reactions reaction);
    Task SaveChangesAsync();
    Task<int> CountAsync(int postId);
}
