using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands
{
    public class RemoveAttributeDefinitionByIdCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
