using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults
{
    public class IsCouponValidResult
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = null!;
    }
}
