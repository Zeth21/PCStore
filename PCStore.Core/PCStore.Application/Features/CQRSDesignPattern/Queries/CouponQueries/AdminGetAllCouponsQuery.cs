using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries
{
    public class AdminGetAllCouponsQuery : IRequest<TaskListResult<AdminGetAllCouponsResult>>
    {
        public string? CouponCode { get; set; }
        public bool IsActive { get; set; } = false;
        public string? CouponValue { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
