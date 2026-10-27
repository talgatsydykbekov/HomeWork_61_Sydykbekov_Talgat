using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace HM_Instagram.BLL.Interfaces;

public interface ICommentRepository
{
    Task<List<Comments>> GetAllComments(int PostId);
    Task<bool> AddComment(CreateCommentDto comment);
    
}