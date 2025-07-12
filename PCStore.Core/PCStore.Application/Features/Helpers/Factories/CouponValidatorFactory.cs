using PCStore.Application.Features.Helpers.Validators.CouponValidator;
using PCStore.Domain.Enum;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Factories
{
    public class CouponValidatorFactory(ProjectDbContext context) : ICouponValidatorFactory
    {
        public ICouponValidator GetValidator(CouponTargetType targetType)
        {
            return targetType switch
            {
                CouponTargetType.AllProducts => new AllProductsCouponValidator(context),
                CouponTargetType.SpecificProducts => new SpecificProductsCouponValidator(context),
                CouponTargetType.Categories => new CategoryCouponValidator(context),
                CouponTargetType.Brands => new BrandCouponValidator(context),
                CouponTargetType.ProductTypes => new ProductTypeCouponValidator(context),
                _ => throw new NotImplementedException("Unknown coupon type!")
            };
        }
    }
}
