using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands
{
    public class RemoveCommentVoteCommand : IRequest<Result>
    {
        public int CommentVoteId { get; set; }
        public required string UserId { get; set; }
    }
}
