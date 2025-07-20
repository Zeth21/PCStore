using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults
{
    public class GetCouponUsageByOrderIdResult
    {
        public decimal? DiscountTotal { get; set; }
        public string? CouponCode { get; set; }
        public int? CouponId { get; set; }
    }
}
