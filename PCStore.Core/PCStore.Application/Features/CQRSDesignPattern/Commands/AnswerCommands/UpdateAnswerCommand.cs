using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AnswerResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands
{
    public class UpdateAnswerCommand : IRequest<TaskResult<UpdateAnswerResult>>
    {
        public int AnswerId { get; set; }
        public required string AnswerUserId { get; set; }
        public required string AnswerText { get; set; }
    }
}
