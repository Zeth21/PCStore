using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponProductValidators
{
    public class ListRemoveCouponProductsValidator : AbstractValidator<ListRemoveCouponProductsCommand>
    {
        public ListRemoveCouponProductsValidator()
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
        }
    }
}
