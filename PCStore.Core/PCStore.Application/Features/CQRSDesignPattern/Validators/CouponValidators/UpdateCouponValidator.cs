using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponValidators
{
    public class UpdateCouponValidator : AbstractValidator<UpdateCouponCommand>
    {
        public UpdateCouponValidator() 
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0)
                .WithMessage("Invalid coupon id!");
            RuleFor(x => x.CouponValue)
                .GreaterThan(0)
                .When(x => x.CouponValue.HasValue)
                .WithMessage("Invalid coupon value!");
            RuleFor(x => x.CouponMaxUsage)
                .GreaterThan(0)
                .When(x => x.CouponMaxUsage.HasValue)
                .WithMessage("Invalid max usage value!");
            RuleFor(x => x.CouponMaxUsagePerUser)
                .GreaterThan(0)
                .When(x => x.CouponMaxUsagePerUser.HasValue)
                .WithMessage("Invalid max usage per user value!");
            RuleFor(x => x.CouponMinOrderAmount)
                .GreaterThanOrEqualTo(0)
                .When(x => x.CouponMinOrderAmount.HasValue)
                .WithMessage("Invalid min order amount value!");
            RuleFor(x => x.Description)
                .MaximumLength(200)
                .When(x => x.Description != null)
                .WithMessage("Coupon description cannot exceed 200 characters!");
            RuleFor(x => x.CouponCode)
                .MaximumLength(50)
                .When(x => x.CouponCode != null)
                .WithMessage("Coupon code cannot exceed 50 characters!");
            RuleFor(x => x.CouponTargetType)
                .IsInEnum()
                .When(x => x.CouponTargetType.HasValue)
                .WithMessage("Invalid coupon target type!");
        }
    }
}
