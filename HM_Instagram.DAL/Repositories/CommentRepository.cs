using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using HM_Instagram.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HM_Instagram.DAL.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly MyUserContext _context;

    public CommentRepository(MyUserContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comments>> GetAllComments(int PostId)
    {
        return await _context.Comments.Include(u => u.User).Where(u => u.PostId == PostId)
            .ToListAsync();
        
    }

    public async Task<bool> AddComment(CreateCommentDto comment)
    {
        var model = new Comments()
        {
            PostId = comment.PostId,
            UserId = comment.UserId,
            Comment = comment.Content,
            Created_DateTime = DateTime.UtcNow,
        };
        _context.Comments.Add(model);
        await _context.SaveChangesAsync();
        return true;
    }
}