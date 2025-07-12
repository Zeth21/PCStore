using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands
{
    public class RemoveAnswerByIdCommand : IRequest<Result>
    {
        public int AnswerId { get; set; }
    }
}
