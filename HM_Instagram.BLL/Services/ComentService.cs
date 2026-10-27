using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;
using Microsoft.EntityFrameworkCore;
namespace HM_Instagram.BLL.Services;
using Microsoft.AspNetCore.Identity;

public class ComentService : ICommentService
{
    private readonly UserManager<MyUser> _userManager;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postrepo;
    public ComentService(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postrepo = postRepository;
    }
    
    
    public async Task<List<GetCommentDto>> GetCommentsAsync(int PostId)
    {
        var comments = await  _commentRepository.GetAllComments(PostId);
        var result = comments.Select(NewComments => new GetCommentDto
        {
            Avatar = NewComments.User.Avatar,
            Content = NewComments.Comment,
            Created = NewComments.Created_DateTime,
            UserName = NewComments.User.UserName,
        }).ToList();
        return result;
    }

    public async Task<bool> AddComment(int postId, string content, int userId)
    {
        if (!await _postrepo.FindPostByIdAsync(postId))
        {
            return false;
        }
        var comment = new CreateCommentDto
        {
            Content = content,
            PostId = postId,
            UserId = userId,
        };
        await _commentRepository.AddComment(comment);
        return true;
    }
    
}