using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Validators.DiscountValidator
{
    public class DiscountUsageCalculator(ProjectDbContext context) : IDiscountUsageCalculator
    {
        public async Task<List<DiscountUsageCalculatorResult>> CalculateDiscountUsage(DiscountUsageCalculatorCommand command)
        {
            var userShopCartItems = await context.ShoppingCartItems
                .Where(x => x.UserId == command.UserId)
                .AsNoTracking()
                .ToListAsync();
            var userShopCartItemsDict = userShopCartItems.ToDictionary(x => x.ProductId);
            var productIds = userShopCartItemsDict.Keys;
            var discountProducts = await context.DiscountProducts
                .Where(x => productIds.Contains(x.ProductId))
                .Select(x => new
                {
                    x.DiscountId,
                    x.Product!.ProductPrice,
                    x.Discount!.DiscountRate,
                    x.Discount.DiscountIsPercentage,
                    x.ProductId
                })
                .AsNoTracking()
                .ToListAsync();
            var result = new List<DiscountUsageCalculatorResult>();
            foreach (var product in discountProducts)
            {
                if (!userShopCartItemsDict.TryGetValue(product.ProductId, out var cartItem))
                    continue;
                var itemCount = cartItem.ItemCount;
                var discountTotal = product.DiscountIsPercentage
                    ? itemCount * (product.ProductPrice * (product.DiscountRate / 100m))
                    : product.DiscountRate;
                var recordCheck = result.SingleOrDefault(x => x.DiscountId == product.DiscountId);
                if(recordCheck is null) 
                {
                    var newRecord = new DiscountUsageCalculatorResult
                    {
                        DiscountId = product.DiscountId,
                        DiscountTotal = discountTotal
                    };
                    result.Add(newRecord);
                    continue;
                }
                recordCheck.DiscountTotal += discountTotal;
            }
            return result;
        }

    }
}
