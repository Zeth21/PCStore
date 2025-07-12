using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentVoteResults;

namespace PCStore.Application.Services.CommentVoteService
{
    public interface ICommentVoteService
    {
        public Task<TaskResult<CreateCommentVoteResult>> CreateCommentVote(CreateCommentVoteCommand request, CancellationToken cancellationToken);
        public Task<Result> RemoveCommentVote(RemoveCommentVoteCommand request, CancellationToken cancellation);
    }
}
