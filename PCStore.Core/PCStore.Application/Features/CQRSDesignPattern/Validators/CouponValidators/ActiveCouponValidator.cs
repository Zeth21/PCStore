using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponValidators
{
    public class ActiveCouponValidator : AbstractValidator<ActiveCouponCommand>
    {
        public ActiveCouponValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
        }
    }
}
