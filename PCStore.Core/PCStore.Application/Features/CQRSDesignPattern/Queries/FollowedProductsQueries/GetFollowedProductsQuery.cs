using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.FollowedProductsQuery
{
    public class GetFollowedProductsQuery : IRequest<TaskListResult<GetFollowedProductsResult>>
    {
        public required string UserId { get; set; }
    }
}
