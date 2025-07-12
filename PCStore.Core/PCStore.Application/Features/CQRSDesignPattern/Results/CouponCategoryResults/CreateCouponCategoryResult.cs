using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponCategoryResults
{
    public class CreateCouponCategoryResult
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
