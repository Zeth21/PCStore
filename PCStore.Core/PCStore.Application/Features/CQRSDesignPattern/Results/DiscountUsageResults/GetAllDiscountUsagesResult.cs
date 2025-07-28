using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.DiscountUsageResults
{
    public class GetAllDiscountUsagesResult
    {
        public int DiscountId { get; set; }
        public string DiscountName { get; set; } = null!;
        public decimal TotalDiscount { get; set; }
        public int OrderCount { get; set; }
        public bool DiscountIsActive { get; set; }
        public decimal DiscountRate { get; set; }
        public bool DiscountIsPercentage { get; set; }
    }
}
