using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponUsageCommand
{
    public class CreateCouponUsageCommand : IRequest<Result>
    {
        public int OrderId { get; set; }
        public required string UserId { get; set; }
        public int CouponId { get; set; }
        public decimal DiscountTotal { get; set; }
    }
}
