using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponValidators
{
    public class RemoveCouponValidator : AbstractValidator<RemoveCouponCommand>
    {
        public RemoveCouponValidator() 
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
        }
    }
}
