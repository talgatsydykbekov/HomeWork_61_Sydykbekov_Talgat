using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.Interfaces;
using HM_Instagram.BLL.Models;

namespace HM_Instagram.BLL.Services;

public class ReactionService : IReactionService
{
    private readonly IReactionRepository _reactionRepository;

    public ReactionService(IReactionRepository reactionRepository)
    {
        _reactionRepository = reactionRepository;
    }

    public async Task<ReactionResultDto?> AddLikeAsync(ReactionsDto reaction)
    {
        if (!await _reactionRepository.PostExistsAsync(reaction.PostId))
            return null;

        var existing = await _reactionRepository.FindAsync(reaction.UserId, reaction.PostId);
        if (existing == null)
        {
            await _reactionRepository.AddAsync(new Reactions
            {
                UserId = reaction.UserId,
                PostId = reaction.PostId
            });
        }
        else
        {
            _reactionRepository.Remove(existing);
        }

        await _reactionRepository.SaveChangesAsync();
        return new ReactionResultDto
        {
            IsLiked = existing == null,
            LikesCount = await _reactionRepository.CountAsync(reaction.PostId)
        };
    }
}
