using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentVoteResults;

namespace PCStore.Application.Services.CommentVoteService
{
    public class CommentVoteService(IMediator mediator) : ICommentVoteService
    {
        private readonly IMediator _mediator = mediator;

        public async Task<TaskResult<CreateCommentVoteResult>> CreateCommentVote(CreateCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<Result> RemoveCommentVote(RemoveCommentVoteCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request, cancellation);
            return result;
        }
    }
}
