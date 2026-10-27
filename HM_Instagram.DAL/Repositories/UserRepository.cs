using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using HM_Instagram.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HM_Instagram.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyUserContext _context;

    public UserRepository(MyUserContext context)
    {
        _context = context;
    }

    public Task<MyUser?> GetByIdAsync(int userId) =>
        _context.Users.AsNoTracking()
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == userId);

    public Task<List<MyUser>> SearchAsync(string? searchTitle)
    {
        var users = _context.Users.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchTitle))
        {
            var term = searchTitle.Trim().ToLower();
            users = users.Where(u =>
                (u.UserName != null && u.UserName.ToLower().Contains(term)) ||
                (u.Email != null && u.Email.ToLower().Contains(term)) ||
                (u.Name != null && u.Name.ToLower().Contains(term)) ||
                (u.Desc != null && u.Desc.ToLower().Contains(term)));
        }

        return users.ToListAsync();
    }

    public Task<Post?> GetPostDetailsAsync(int postId) =>
        _context.Posts.AsNoTracking()
            .Include(p => p.Reactions)
            .Include(p => p.User)
            .Include(p => p.Comments!).ThenInclude(c => c.User)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == postId);
}
