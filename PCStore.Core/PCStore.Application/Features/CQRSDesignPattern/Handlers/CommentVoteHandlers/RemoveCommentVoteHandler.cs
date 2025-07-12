using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Domain.Enum;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CommentVoteHandlers
{
    public class RemoveCommentVoteHandler(ProjectDbContext projectDbContext) : IRequestHandler<RemoveCommentVoteCommand, Result>
    {
        private readonly ProjectDbContext _projectDbContext = projectDbContext;

        public async Task<Result> Handle(RemoveCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var vote = await _projectDbContext.CommentVotes
                .Where(x => x.CommentVoteUserId == request.UserId && x.CommentVoteId == request.CommentVoteId)
                .FirstOrDefaultAsync(cancellationToken);
            if (vote is null)
                return Result.NotFound(message: "Vote not found!");
            var comment = await _projectDbContext.Comments.FindAsync(vote.CommentVoteCommentId);
            if (comment is null)
                return Result.NotFound(message: "Comment not found!");
            if (vote.CommentVoteValue == VoteType.Up)
                comment.CommentUpVoteCount -= 1;
            else
                comment.CommentDownVoteCount -= 1;
            _projectDbContext.CommentVotes.Remove(vote);
            await _projectDbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(message: "Vote removed successfully!");
        }
    }
}
