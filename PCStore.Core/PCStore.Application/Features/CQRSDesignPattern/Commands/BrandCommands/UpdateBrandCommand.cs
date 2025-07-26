using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.BrandCommands
{
    public class UpdateBrandCommand : IRequest<TaskResult<UpdateBrandResult>>
    {
        public int BrandId { get; set; }
        public required string BrandName { get; set; }
    }
}
