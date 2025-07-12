using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeHandlers;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults;

namespace PCStore.Application.Services.TypeService
{
    public interface ITypeService
    {
        public Task<TaskResult<CreateProductTypeResult>> CreateProductType(CreateProductTypeCommand request, CancellationToken cancellationToken);
        public Task<Result> RemoveProductType(RemoveProductTypeCommand request, CancellationToken cancellationToken);
        public Task<TaskListResult<GetTypeAttributesByIdResult>> CreateTypeAttribute(CreateTypeAttributeCommand request, CancellationToken cancellationToken);
        public Task<TaskListResult<GetTypeAttributesByIdResult>> GetTypeAttributesById(GetTypeAttributesByIdQuery request, CancellationToken cancellationToken);
        public Task<Result> RemoveTypeAttributesById(RemoveTypeAttributeCommand request, CancellationToken cancellationToken);
        public Task<TaskResult<UpdateTypeResult>> UpdateProductType(UpdateTypeCommand request, CancellationToken cancellationToken);

    }
}
