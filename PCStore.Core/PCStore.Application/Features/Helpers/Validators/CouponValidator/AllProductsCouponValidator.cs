using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.CouponValidator
{
    public class AllProductsCouponValidator(ProjectDbContext context) : ICouponValidator
    {
        public async Task<CouponValidatorResult> IsValid(string userId,Coupon coupon, List<CouponValidatorCommand> products)
        {
            var userUsage = await context.CouponUsages
                .Where(x => x.CouponUsageCouponId == coupon.CouponId && x.CouponUsageUserId == userId)
                .CountAsync();
            if (coupon.CouponMaxUsagePerUser <= userUsage)
                return CouponValidatorResult.NotValid("You have reached coupons usage limit!");
            var couponProductsTotalCost = products.Sum(x => x.ProductPrice * x.ItemCount);
            if (coupon.CouponMinOrderAmount > couponProductsTotalCost)
                return CouponValidatorResult.NotValid("You are under coupons minimum order amount!");
            decimal totalDiscount;
            if (!coupon.CouponIsPercentage)
                totalDiscount = coupon.CouponValue;
            else
                totalDiscount = couponProductsTotalCost * (coupon.CouponValue / 100m);
            return CouponValidatorResult.Valid(totalDiscount:totalDiscount);
        }
    }
}
