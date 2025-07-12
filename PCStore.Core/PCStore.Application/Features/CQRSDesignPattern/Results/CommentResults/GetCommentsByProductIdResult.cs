namespace PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults
{
    public class GetCommentsByProductIdResult
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string? UserName { get; set; }
        public int CommentAnswerCount { get; set; }
        public int CommentUpVoteCount { get; set; }
        public int CommentDownVoteCount { get; set; }
    }
}
