using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponBrandCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponBrandValidators
{
    public class RemoveCouponBrandValidator : AbstractValidator<RemoveCouponBrandCommand>
    {
        public RemoveCouponBrandValidator() 
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
        }
    }
}
