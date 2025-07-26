using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.BrandCommands
{
    public class CreateBrandCommand : IRequest<TaskResult<CreateBrandResult>>
    {
        public required string BrandName { get; set; }
    }
}
