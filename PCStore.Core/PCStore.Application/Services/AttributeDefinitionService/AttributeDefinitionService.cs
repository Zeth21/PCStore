using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Services.AttributeDefinitionService
{
    public class AttributeDefinitionService(IMediator mediator) : IAttributeDefinitionService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<TaskResult<UpdateAttributeDefinitionResult>> UpdateAttributeDefinition(UpdateAttributeDefinitionCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public Task<Result> RemoveAttributeDefinition(RemoveAttributeDefinitionByIdCommand request, CancellationToken cancellationToken)
        {
            var result = _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<TaskResult<CreateAttributeDefinitionResult>> CreateAttributeDefinition(CreateAttributeDefinitionCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }


    }
}
