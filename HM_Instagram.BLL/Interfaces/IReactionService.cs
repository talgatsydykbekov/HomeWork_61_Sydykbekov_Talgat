using HM_Instagram.BLL.DTOs;

namespace HM_Instagram.BLL.Interfaces;

public interface IReactionService
{
    Task<ReactionResultDto?> AddLikeAsync(ReactionsDto reaction);
}
