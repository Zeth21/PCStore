using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands
{
    public class UpdateCommentCommand : IRequest<TaskResult<UpdateCommentResult>>
    {
        public int CommentId { get; set; }
        public required string CommentUserId { get; set; }
        public required string CommentText { get; set; }
    }
}
