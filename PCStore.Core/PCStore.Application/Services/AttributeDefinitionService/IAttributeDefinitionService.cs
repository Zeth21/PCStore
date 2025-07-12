using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Services.AttributeDefinitionService
{
    public interface IAttributeDefinitionService
    {
        public Task<TaskResult<UpdateAttributeDefinitionResult>> UpdateAttributeDefinition(UpdateAttributeDefinitionCommand request, CancellationToken cancellationToken);
        public Task<TaskResult<CreateAttributeDefinitionResult>> CreateAttributeDefinition(CreateAttributeDefinitionCommand request, CancellationToken cancellationToken);
        public Task<Result> RemoveAttributeDefinition(RemoveAttributeDefinitionByIdCommand request, CancellationToken cancellationToken);

    }
}
