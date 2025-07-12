using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeHandlers
{
    public class CreateTypeAttributeCommand : IRequest<TaskListResult<GetTypeAttributesByIdResult>>
    {
        public int TypeId { get; set; }
        public List<int> AttributeIds { get; set; } = [];
    }
}
