using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Results;
using PCStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.CouponValidator
{
    public interface ICouponValidator
    {
        Task<CouponValidatorResult> IsValid(string userId,Coupon coupon, List<CouponValidatorCommand> products);
    }
}
