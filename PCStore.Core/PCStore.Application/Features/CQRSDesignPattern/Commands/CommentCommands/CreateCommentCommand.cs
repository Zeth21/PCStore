using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands
{
    public class CreateCommentCommand : IRequest<TaskResult<CreateCommentResult>>
    {
        public required string CommentText { get; set; }
        public bool CommentIsQuestion { get; set; } = false;
        public required string CommentUserId { get; set; }
        public int CommentProductId { get; set; }

    }
}
