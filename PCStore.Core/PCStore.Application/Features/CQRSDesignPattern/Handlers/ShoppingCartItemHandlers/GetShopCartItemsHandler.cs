using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ShoppingCartItemsQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ShoppingCartItemResults;
using PCStore.Application.Features.Helpers.Factories;
using PCStore.Application.Features.Helpers.Validators.CouponValidator;
using PCStore.Application.Features.Helpers.Validators.CouponValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ShoppingCartItemHandlers
{
    public class GetShopCartItemsHandler(ProjectDbContext context, IMapper mapper, IDiscountChecker checker, ICouponValidatorFactory validatorFactory) : IRequestHandler<GetShopCartItemsQuery, TaskResult<BulkGetShopCartItemsResult>>
    {
        public async Task<TaskResult<BulkGetShopCartItemsResult>> Handle(GetShopCartItemsQuery request, CancellationToken cancellationToken)
        {
            var userExists = await context.Users
                .AnyAsync(x => x.Id == request.UserId);
            if (!userExists)
                return TaskResult<BulkGetShopCartItemsResult>.Fail("Invalid user!");
            var products = await context.Products
                .Include(x => x.ShoppingCartItems)
                .Where(x => x.ShoppingCartItems != null && x.ShoppingCartItems.Any(sc => sc.UserId == request.UserId))
                .ToListAsync(cancellationToken);
            if (products.Count == 0)
                return TaskResult<BulkGetShopCartItemsResult>.NotFound("No products found!");
            var result = new BulkGetShopCartItemsResult();
            var checkDiscount = await checker.CheckDiscount(mapper.Map<List<DiscountValidatorCommand>>(products));
            var couponProductList = mapper.Map<List<CouponValidatorCommand>>(checkDiscount);
            if (request.CouponId is not null)
            {
                foreach (var item in couponProductList)
                {
                    var product = products.Where(x => x.ProductId == item.ProductId).SingleOrDefault();
                    if (product is not null)
                        item.ItemCount = product.ShoppingCartItems!.FirstOrDefault()!.ItemCount;
                }
                var coupon = await context.Coupons.FindAsync(request.CouponId, cancellationToken);
                if (coupon is not null)
                {
                    var validator = validatorFactory.GetValidator(coupon.CouponTargetType);
                    var couponDiscount = await validator.IsValid(request.UserId, coupon, couponProductList);
                    if (couponDiscount.IsValid)
                        result.TotalCouponDiscount = couponDiscount.TotalDiscount;
                }
            }
            var dtoList = mapper.Map<List<GetShopCartItemsResult>>(checkDiscount);
            foreach (var dto in dtoList)
            {
                var product = products.Where(x => x.ProductId == dto.ProductId).SingleOrDefault();
                if (product is not null)
                {
                    dto.Id = product.ShoppingCartItems!.FirstOrDefault()!.Id;
                    dto.ItemCount = product.ShoppingCartItems!.FirstOrDefault()!.ItemCount;
                }
            }
            result.CartItems = dtoList;
            return TaskResult<BulkGetShopCartItemsResult>.Success("All products found successfully!", data: result);
        }

    }
}
