using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using HM_Instagram.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HM_Instagram.DAL.Repositories;

public class ReactionRepository : IReactionRepository
{
    private readonly MyUserContext _context;

    public ReactionRepository(MyUserContext context)
    {
        _context = context;
    }

    public Task<bool> PostExistsAsync(int postId) =>
        _context.Posts.AnyAsync(p => p.Id == postId);

    public Task<Reactions?> FindAsync(int userId, int postId) =>
        _context.Reactions.FirstOrDefaultAsync(r => r.UserId == userId && r.PostId == postId);

    public async Task AddAsync(Reactions reaction) =>
        await _context.Reactions.AddAsync(reaction);

    public void Remove(Reactions reaction) => _context.Reactions.Remove(reaction);

    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public Task<int> CountAsync(int postId) =>
        _context.Reactions.CountAsync(r => r.PostId == postId);
}
