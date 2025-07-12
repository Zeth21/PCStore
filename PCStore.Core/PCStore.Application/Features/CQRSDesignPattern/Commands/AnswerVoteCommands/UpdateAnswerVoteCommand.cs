using PCStore.Domain.Enum;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerVoteCommands
{
    class UpdateAnswerVoteCommand
    {
        public int AnswerVoteId { get; set; }
        public VoteType AnswerVoteValue { get; set; }
    }
}
