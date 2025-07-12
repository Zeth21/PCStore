using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Handlers.AttributeDefinitionHandlers.UpdateDTOs;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands
{
    public class UpdateAttributeDefinitionCommand : IRequest<TaskResult<UpdateAttributeDefinitionResult>>
    {
        public int Id { get; set; }
        public List<AttributeDefinitionProperties>? Properties { get; set; }
    }
}
