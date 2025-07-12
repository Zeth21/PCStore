namespace PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults
{
    public class CreateAnswerResult
    {
        public int AnswerId { get; set; }
        public required string AnswerText { get; set; }
        public DateTime AnswerDate { get; set; } = DateTime.Now;
        public string? AnswerUserName { get; set; }
        public int AnswerCommentId { get; set; }
        public int AnswerUpVoteCount { get; set; }
        public int AnswerDownVoteCount { get; set; }
    }
}
