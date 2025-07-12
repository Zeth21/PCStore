using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductTypeResults
{
    public class CreateCouponProductTypeResult
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
