using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CommentVoteResults;
using PCStore.Domain.Enum;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands
{
    public class CreateCommentVoteCommand : IRequest<TaskResult<CreateCommentVoteResult>>
    {
        public VoteType CommentVoteValue { get; set; }
        
        [JsonIgnore]
        public string? CommentVoteUserId { get; set; } = "";
        public int CommentVoteCommentId { get; set; }
    }
}
