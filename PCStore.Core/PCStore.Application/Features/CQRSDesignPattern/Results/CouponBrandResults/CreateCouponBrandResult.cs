using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponBrandResults
{
    public class CreateCouponBrandResult
    {
        public int BrandId { get; set; }
        public required string BrandName { get; set; }
    }
}
