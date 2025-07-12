using PCStore.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults
{
    public class UpdateCouponResult
    {
        public int CouponId { get; set; }
        public decimal CouponValue { get; set; }
        public bool CouponIsPercentage { get; set; } = false;
        public int CouponMaxUsage { get; set; }
        public int CouponMaxUsagePerUser { get; set; } = 1;
        public int CouponMinOrderAmount { get; set; } = 0;
        public DateTime CouponStartTime { get; set; } = DateTime.Now;
        public DateTime? CouponEndTime { get; set; }
        public bool CouponIsActive { get; set; } = true;
        public required string Description { get; set; }
        public required string CouponCode { get; set; }
        public CouponTargetType CouponTargetType { get; set; }
        public CouponTargetType? OldTarget { get; set; }
    }
}
