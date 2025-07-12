using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.FollowedProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.FollowedProductsQuery;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.FollowedProductsService
{
    public class FollowedService(IMediator mediator) : IFollowedService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<TaskResult<CreateFollowedProductResult>> CreateFollowedProduct(CreateFollowedProductCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskListResult<GetFollowedProductsResult>> GetFollowedProducts(GetFollowedProductsQuery request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request,cancellation);
            return result;
        }

        public async Task<Result> RemoveFollowedProduct(RemoveFollowedProductCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request,cancellation);
            return result;
        }
    }
}
