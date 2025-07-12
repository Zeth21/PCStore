using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeHandlers;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults;

namespace PCStore.Application.Services.TypeService
{
    public class TypeService(IMediator mediator) : ITypeService
    {
        private readonly IMediator _mediator = mediator;
        public Task<Result> RemoveProductType(RemoveProductTypeCommand request, CancellationToken cancellationToken)
        {
            var result = _mediator.Send(request, cancellationToken);
            return result;
        }
        public async Task<TaskResult<CreateProductTypeResult>> CreateProductType(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<TaskListResult<GetTypeAttributesByIdResult>> CreateTypeAttribute(CreateTypeAttributeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<TaskListResult<GetTypeAttributesByIdResult>> GetTypeAttributesById(GetTypeAttributesByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public async Task<Result> RemoveTypeAttributesById(RemoveTypeAttributeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        public Task<TaskResult<UpdateTypeResult>> UpdateProductType(UpdateTypeCommand request, CancellationToken cancellationToken)
        {
            var result = _mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
