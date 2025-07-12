using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries
{
    public class GetProductAttributesByIdQuery : IRequest<TaskListResult<GetProductAttributesResult>>
    {
        public int ProductId { get; set; }
    }
}
