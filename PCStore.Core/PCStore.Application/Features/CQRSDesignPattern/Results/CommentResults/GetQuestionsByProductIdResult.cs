using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CommentResults
{
    public class GetQuestionsByProductIdResult
    {
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public DateTime QuestionDate { get; set; }
        public string? UserName { get; set; }
        public int QuestionUpVoteCount { get; set; }
        public IEnumerable<GetAnswersByQuestionIdResult>? Answers { get; set; }

    }
}
