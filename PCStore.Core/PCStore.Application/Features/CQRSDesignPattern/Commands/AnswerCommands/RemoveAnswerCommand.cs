namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands
{
    public class RemoveAnswerCommand
    {
        public int AnswerId { get; set; }

        public RemoveAnswerCommand(int answerId)
        {
            AnswerId = answerId;
        }
    }
}
