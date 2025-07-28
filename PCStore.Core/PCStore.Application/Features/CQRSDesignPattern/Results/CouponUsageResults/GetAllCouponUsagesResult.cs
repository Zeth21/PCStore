using PCStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults
{
    public class GetAllCouponUsagesResult
    {
        public int CouponId { get; set; }
        public decimal CouponValue { get; set; }
        public bool CouponIsPercentage { get; set; }
        public DateTime CouponStartTime { get; set; }
        public DateTime? CouponEndTime { get; set; }
        public bool CouponIsActive { get; set; }
        public required string CouponCode { get; set; }
        public CouponTargetType CouponTargetType { get; set; }
        public decimal TotalDiscount { get; set; }
        public int OrderCount { get; set; }
        public int UniqueUserCount { get; set; }
    }
}
