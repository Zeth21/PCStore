using PCStore.Application.Features.Helpers.Validators.CouponValidator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.CouponValidator.Results
{
    public class CouponValidatorResult
    {
        public bool IsValid { get; set; }
        public decimal? TotalDiscount { get; set; }
        public required string Message { get; set; }
        public List<CouponValidatorCommand>? Data { get; set; }
        public static CouponValidatorResult Valid(List<CouponValidatorCommand>? data = null, decimal? totalDiscount = null, string message = "Coupon is valid!") 
        {
            return new CouponValidatorResult
            {
                IsValid = true,
                Message = message,
                TotalDiscount = totalDiscount,
                Data = data
            };
        }

        public static CouponValidatorResult NotValid(string message = "Coupon is not valid!") 
        {
            return new CouponValidatorResult
            {
                IsValid = false,
                Message = message
            };
        }
    }
}
