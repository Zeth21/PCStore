using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ShoppingCartItemResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.ShoppingCartItemsQueries
{
    public class GetShopCartItemsQuery : IRequest<TaskResult<BulkGetShopCartItemsResult>>
    {
        public required string UserId { get; set; }
        public int? CouponId { get; set; }
    }
}
