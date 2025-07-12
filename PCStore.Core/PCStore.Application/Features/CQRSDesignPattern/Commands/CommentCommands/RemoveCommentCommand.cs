using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands
{
    public class RemoveCommentCommand : IRequest<Result>
    {
        public int CommentId { get; set; }
        public required string UserId { get; set; }
    }
}
