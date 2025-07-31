using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponUsageResults;
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
    public class IsCouponValidHandler(ProjectDbContext context, IDiscountChecker checker, ICouponValidatorFactory validatorFactory, IMapper mapper) : IRequestHandler<IsCouponValidQuery, TaskResult<IsCouponValidResult>>
    {
        public async Task<TaskResult<IsCouponValidResult>> Handle(IsCouponValidQuery request, CancellationToken cancellationToken)
        {
            var coupon = await context.Coupons
                .SingleOrDefaultAsync(x => x.CouponCode == request.CouponCode, cancellationToken);
            if (coupon is null)
                return TaskResult<IsCouponValidResult>.Fail("Coupon not found!");
            var productList = await context.Products
                .Include(x => x.ShoppingCartItems)
                .Where(x => x.ShoppingCartItems != null && x.ShoppingCartItems.Any(s => s.UserId == request.UserId))
                .ToListAsync();
            if (productList.Count <= 0)
                return TaskResult<IsCouponValidResult>.Fail("There is no products in the cart!");
            var checkerList = await checker.CheckDiscount(mapper.Map<List<DiscountValidatorCommand>>(productList));
            var validator = validatorFactory.GetValidator(coupon.CouponTargetType);
            var couponCheckList = mapper.Map<List<CouponValidatorCommand>>(checkerList);
            foreach(var cpl in couponCheckList) 
            {
                var product = productList.Where(x => x.ProductId == cpl.ProductId).SingleOrDefault();
                if (product is not null)
                    cpl.ItemCount = product.ShoppingCartItems!.FirstOrDefault()!.ItemCount;
            }
            var isValid = await validator.IsValid(request.UserId, coupon, couponCheckList);
            if (!isValid.IsValid)
                return TaskResult<IsCouponValidResult>.Fail(isValid.Message);
            var result = mapper.Map<IsCouponValidResult>(coupon);
            return TaskResult<IsCouponValidResult>.Success("Coupon is valid!", result);
        }
    }
}
