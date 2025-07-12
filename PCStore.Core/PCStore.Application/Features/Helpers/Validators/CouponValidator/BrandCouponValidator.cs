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
    public class BrandCouponValidator(ProjectDbContext context) : ICouponValidator
    {
        public async Task<CouponValidatorResult> IsValid(string userId, Coupon coupon, List<CouponValidatorCommand> products)
        {
            var userCouponUsage = await context.CouponUsages
                .CountAsync(x => x.CouponUsageUserId == userId && x.CouponUsageCouponId == coupon.CouponId);
            if (coupon.CouponMaxUsagePerUser <= userCouponUsage)
                return CouponValidatorResult.NotValid("You have reached coupons usage limit!");
            var validBrandIds = await context.CouponBrands
                .Where(x => x.CouponId == coupon.CouponId)
                .Select(x => x.BrandId)
                .ToListAsync();
            if (validBrandIds.Count <= 0)
                return CouponValidatorResult.NotValid("Products are invalid for coupon!");
            var validProductsTotalCost = products.Where(x => validBrandIds.Contains(x.ProductBrandId)).Sum(x => x.ProductPrice * x.ItemCount);
            if (coupon.CouponMinOrderAmount > validProductsTotalCost)
                return CouponValidatorResult.NotValid("You are under coupons minimum order amount!");
            decimal totalDiscount;
            if (!coupon.CouponIsPercentage)
                totalDiscount = coupon.CouponValue;
            else
                totalDiscount = validProductsTotalCost * (coupon.CouponValue / 100m);
            return CouponValidatorResult.Valid(totalDiscount:totalDiscount);
        }
    }
}
