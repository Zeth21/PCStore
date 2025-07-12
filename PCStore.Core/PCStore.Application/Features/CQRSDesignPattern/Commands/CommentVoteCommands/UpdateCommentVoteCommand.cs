using PCStore.Domain.Enum;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CommentVoteCommands
{
    public class UpdateCommentVoteCommand
    {
        public int CommentVoteId { get; set; }
        public VoteType CommentVoteValue { get; set; }
    }
}
