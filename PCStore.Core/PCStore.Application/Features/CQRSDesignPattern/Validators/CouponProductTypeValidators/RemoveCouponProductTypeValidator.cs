using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponProductTypeValidators
{
    public class RemoveCouponProductTypeValidator : AbstractValidator<RemoveCouponProductTypeCommand>
    {
        public RemoveCouponProductTypeValidator() 
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
        }
    }
}
