using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands
{
    public class RemoveAnswerVoteCommand : IRequest<Result>
    {
        public int AnswerVoteId { get; set; }
        public required string UserId { get; set; }
    }
}
