using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries
{
    public class IsCouponValidQuery : IRequest<TaskResult<IsCouponValidResult>>
    {
        public required string UserId { get; set; }
        public string? CouponCode { get; set; }
    }
}
