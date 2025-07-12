using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries
{
    public class GetTypeAttributesByIdQuery : IRequest<TaskListResult<GetTypeAttributesByIdResult>>
    {
        public int TypeId { get; set; }
    }
}
