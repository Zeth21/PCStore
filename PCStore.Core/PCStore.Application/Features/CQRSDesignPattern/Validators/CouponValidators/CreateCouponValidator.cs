using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponValidators
{
    public class CreateCouponValidator : AbstractValidator<CreateCouponCommand>
    {
        public CreateCouponValidator() 
        {
            RuleFor(x => x.CouponValue)
                .GreaterThan(0).WithMessage("Invalid coupon value id!");
            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Coupon description cannot exceed 200 characters!");
        }
    }
}
