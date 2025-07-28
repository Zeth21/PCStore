using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Commands;
using PCStore.Application.Features.Helpers.Validators.DiscountValidator.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;


namespace PCStore.Application.Features.Helpers.Validators.DiscountValidator
{
    public class DiscountChecker(ProjectDbContext context, IMapper mapper) : IDiscountChecker
    {
        public async Task<List<DiscountValidatorResult>> CheckDiscount(List<DiscountValidatorCommand> products)
        {
            var productIds = products.Select(x => x.ProductId).ToList();
            var discounts = await context.DiscountProducts
                .Include(x => x.Discount)
                .Where(x =>  x.Discount!.DiscountIsActive && productIds.Contains(x.ProductId))
                .ToListAsync();
            var result = new List<DiscountValidatorResult>();
            foreach(var product in products) 
            {
                var productDiscount = discounts.Where(x => x.ProductId == product.ProductId && x.Discount != null).Select(x => x.Discount).FirstOrDefault();
                if(productDiscount is null)
                {
                    result.Add(mapper.Map<DiscountValidatorResult>(product));
                    continue;
                }
                var calculatedDiscount = CalculateDiscount(product, productDiscount);
                result.Add(calculatedDiscount);
            }
            return result;
        }
        public DiscountValidatorResult CalculateDiscount(DiscountValidatorCommand product, Discount discount)
        {
            var result = mapper.Map<DiscountValidatorResult>(product);
            result.OldPrice = product.ProductPrice;
            result.DiscountRate = discount.DiscountRate;
            result.IsDiscountPercentage = discount.DiscountIsPercentage;
            if (!discount.DiscountIsPercentage) 
            {
                result.ProductPrice = (decimal)result.OldPrice - discount.DiscountRate;
                return result;
            }
            result.ProductPrice = (decimal)result.OldPrice * (1 - (discount.DiscountRate / 100m));
            return result;
        }
    }
}
