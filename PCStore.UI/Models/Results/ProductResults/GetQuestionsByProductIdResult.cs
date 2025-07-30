namespace PCStore.UI.Models.Results.ProductResults
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
