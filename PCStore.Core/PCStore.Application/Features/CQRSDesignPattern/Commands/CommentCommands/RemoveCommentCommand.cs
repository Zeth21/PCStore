using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands
{
    public class RemoveCommentCommand : IRequest<Result>
    {
        public int CommentId { get; set; }

        [JsonIgnore]
        public string? UserId { get; set; } = "";
    }
}
