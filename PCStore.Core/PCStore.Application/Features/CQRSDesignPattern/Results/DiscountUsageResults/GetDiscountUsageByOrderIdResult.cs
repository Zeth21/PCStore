using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.DiscountUsageResults
{
    public class GetDiscountUsageByOrderIdResult
    {
        public int DiscountId { get; set; }
        public required string DiscountName { get; set; }
        public decimal DiscountTotal { get; set; }
    }
}
