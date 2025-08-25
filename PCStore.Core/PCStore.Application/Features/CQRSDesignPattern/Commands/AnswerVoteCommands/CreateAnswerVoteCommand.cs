using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerVoteResults;
using PCStore.Domain.Enum;
using System.Text.Json.Serialization;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands
{
    public class CreateAnswerVoteCommand : IRequest<TaskResult<CreateAnswerVoteResult>>
    {
        public VoteType AnswerVoteValue { get; set; }
        [JsonIgnore]
        public string? AnswerVoteUserId { get; set; } = "";
        public int AnswerVoteAnswerId { get; set; }
    }
}
