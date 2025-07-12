using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.Helpers.Factories;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponUsageHandlers
{
    public class IsCouponValidHandler(ProjectDbContext context, IDiscountChecker checker, ICouponValidatorFactory validatorFactory, IMapper mapper) : IRequestHandler<IsCouponValidQuery, Result>
    {
        public async Task<Result> Handle(IsCouponValidQuery request, CancellationToken cancellationToken)
        {
            var coupon = await context.Coupons
                .FindAsync(request.CouponId, cancellationToken);
            if (coupon is null)
                return Result.Fail("Coupon not found!");
            var productList = await context.Products
                .Include(x => x.ShoppingCartItems)
                .Where(x => x.ShoppingCartItems != null && x.ShoppingCartItems.Any(s => s.UserId == request.UserId))
                .ToListAsync();
            if (productList.Count <= 0)
                return Result.Fail("There is no products in the cart!");
            var checkerList = await checker.CheckDiscount(mapper.Map<List<DiscountValidatorCommand>>(productList));
            var validator = validatorFactory.GetValidator(coupon.CouponTargetType);
            var couponCheckList = mapper.Map<List<CouponValidatorCommand>>(checkerList);
            foreach(var cpl in couponCheckList) 
            {
                var product = productList.Where(x => x.ProductId == cpl.ProductId).SingleOrDefault();
                if (product is not null)
                    cpl.ItemCount = product.ShoppingCartItems!.FirstOrDefault()!.ItemCount;
            }
            var result = await validator.IsValid(request.UserId, coupon, couponCheckList);
            if (!result.IsValid)
                return Result.Fail(result.Message);
            return Result.Success(result.Message);
        }
    }
}
