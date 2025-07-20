using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries
{
    public class GetCouponUsageByOrderIdQuery : IRequest<TaskResult<GetCouponUsageByOrderIdResult>>
    {
        public int OrderId { get; set; }
    }
}
