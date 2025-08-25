using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands
{
    public class RemoveCommentVoteCommand : IRequest<Result>
    {
        public int CommentVoteId { get; set; }

        [JsonIgnore]
        public string? UserId { get; set; } = "";
    }
}
