using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results
{
    public class DiscountUsageCalculatorResult
    {
        public int? OrderId { get; set; }
        public int DiscountId { get; set; }
        public decimal DiscountTotal { get; set; }
    }
}
