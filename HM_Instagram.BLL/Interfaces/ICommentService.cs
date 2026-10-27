using HM_Instagram.BLL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HM_Instagram.BLL.Interfaces;

public interface ICommentService
{
    Task<List<GetCommentDto>> GetCommentsAsync(int PostId);
    Task<bool> AddComment(int postId, string content,int userId);
}