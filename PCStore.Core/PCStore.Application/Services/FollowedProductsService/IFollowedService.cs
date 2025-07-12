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
    public interface IFollowedService
    {
        public Task<TaskResult<CreateFollowedProductResult>> CreateFollowedProduct(CreateFollowedProductCommand request, CancellationToken cancellation);
        public Task<Result> RemoveFollowedProduct(RemoveFollowedProductCommand request, CancellationToken cancellation);
        public Task<TaskListResult<GetFollowedProductsResult>> GetFollowedProducts(GetFollowedProductsQuery request, CancellationToken cancellation);
    }
}
