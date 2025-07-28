using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults;
using PCStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries
{
    public class GetAllCouponUsagesQuery : IRequest<TaskListResult<GetAllCouponUsagesResult>>
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        public bool? CouponIsPercentage { get; set; }
        public bool? CouponIsActive { get; set; }
        public string? CouponCode { get; set; }
        public CouponTargetType? CouponTargetType { get; set; }
        public bool? OrderByTotalDiscount { get; set; }

    }
}
