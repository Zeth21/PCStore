using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCategoryCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponCategoryValidators
{
    public class CreateCouponCategoryValidator : AbstractValidator<CreateCouponCategoryCommand>
    {
        public CreateCouponCategoryValidator() 
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Invalid category id!");
            RuleFor(x => x.CouponId)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
        }
    }
}
