using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults
{
    public class AdminGetCouponByIdResult
    {
        public int CouponId { get; set; }
        public decimal CouponValue { get; set; }
        public bool CouponIsPercentage { get; set; }
        public int CouponMaxUsage { get; set; }
        public int CouponMaxUsagePerUser { get; set; }
        public int CouponMinOrderAmount { get; set; }
        public DateTime CouponStartTime { get; set; }
        public DateTime? CouponEndTime { get; set; }
        public bool CouponIsActive { get; set; }
        public required string Description { get; set; }
        public required string CouponCode { get; set; }
    }
}
