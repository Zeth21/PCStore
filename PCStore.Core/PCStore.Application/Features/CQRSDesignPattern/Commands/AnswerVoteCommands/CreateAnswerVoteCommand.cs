using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerVoteResults;
using PCStore.Domain.Enum;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands
{
    public class CreateAnswerVoteCommand : IRequest<TaskResult<CreateAnswerVoteResult>>
    {
        public VoteType AnswerVoteValue { get; set; }
        public required string AnswerVoteUserId { get; set; }
        public int AnswerVoteAnswerId { get; set; }
    }
}
