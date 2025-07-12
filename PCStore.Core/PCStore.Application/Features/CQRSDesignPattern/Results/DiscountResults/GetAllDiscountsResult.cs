using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults
{
    public class GetAllDiscountsResult
    {
        public int DiscountId { get; set; }
        public required string DiscountName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public bool DiscountIsActive { get; set; }
        public bool DiscountIsPercentage { get; set; }
        public decimal DiscountRate { get; set; }
        public required string Description { get; set; }
    }
}
