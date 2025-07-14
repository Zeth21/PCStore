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
            var shopCartItems = await context.ShoppingCartItems
                .Include(x => x.Product)
                    .ThenInclude(p => p!.Brand)
                .Include(x => x.Product)
                    .ThenInclude(p => p!.Category)
                .Where(x => x.UserId == request.UserId)
                .ToListAsync(cancellationToken);
            if (shopCartItems.Count <= 0)
                return TaskResult<BulkGetShopCartItemsResult>.NotFound("No products found in the cart!");
            var products = shopCartItems.Select(x => x.Product).ToList();
            var checkDiscountList = await checker.CheckDiscount(mapper.Map<List<DiscountValidatorCommand>>(products));
            var resultList = mapper.Map<List<GetShopCartItemsResult>>(shopCartItems);
            foreach (var product in resultList)
            {
                var checkDiscount = checkDiscountList.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (checkDiscount is not null)
                    mapper.Map(checkDiscount, product);
            }
            var result = new BulkGetShopCartItemsResult { };
            if (request.CouponId is not null && request.CouponId != 0)
            {
                var coupon = await context.Coupons
                    .SingleOrDefaultAsync(x => x.CouponId == request.CouponId);
                if (coupon is null)
                    return TaskResult<BulkGetShopCartItemsResult>.Fail("Invalid coupon!");
                var validator = validatorFactory.GetValidator(coupon.CouponTargetType);
                var validatorList = mapper.Map<List<CouponValidatorCommand>>(resultList);
                foreach(var item in validatorList) 
                {
                    var product = products.SingleOrDefault(x => x!.ProductId == item.ProductId);
                    if(product is not null) 
                    {
                        item.ProductTypeId = product.ProductTypeId;
                        item.ProductBrandId = product.ProductBrandId;
                        item.ProductCategoryId = product.ProductCategoryId;
                    }
                }
                var validatorResult = await validator.IsValid(request.UserId, coupon,validatorList);
                if (validatorResult.IsValid)
                {
                    result.TotalCouponDiscount = validatorResult.TotalDiscount;
                }
            }
            result.CartItems = resultList;
            return TaskResult<BulkGetShopCartItemsResult>.Success("All products found successfully!",data:result);
        }

    }
}
