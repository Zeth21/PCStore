using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CategoryResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<TaskResult<CreateCategoryResult>>
    {
        public int? ParentCategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
