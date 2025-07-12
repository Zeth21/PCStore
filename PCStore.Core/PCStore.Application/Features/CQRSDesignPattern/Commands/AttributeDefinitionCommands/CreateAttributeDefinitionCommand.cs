using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands
{
    public class CreateAttributeDefinitionCommand : IRequest<TaskResult<CreateAttributeDefinitionResult>>
    {
        public string? Name { get; set; }
        public string? DataType { get; set; }
        public bool IsRequired { get; set; }
        public string? Unit { get; set; }
    }
}
