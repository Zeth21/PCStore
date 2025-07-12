namespace PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults
{
    public class GetAnswersByQuestionIdResult
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? UserName { get; set; }
        public DateTime Date { get; set; }

    }
}
