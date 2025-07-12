namespace PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults
{
    public class GetAnswersByCommentIdResult
    {
        public int AnswerId { get; set; }
        public string? AnswerText { get; set; }
        public DateTime AnswerDate { get; set; }
        public string? AnswerUserName { get; set; }
        public int AnswerUpVoteCount { get; set; }
        public int AnswerDownVoteCount { get; set; }
    }
}
