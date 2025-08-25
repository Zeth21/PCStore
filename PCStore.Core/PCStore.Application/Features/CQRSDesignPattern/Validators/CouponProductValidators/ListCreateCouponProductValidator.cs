using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponProductValidators
{
    public class ListCreateCouponProductValidator : AbstractValidator<ListCreateCouponProductCommand>
    {
        public ListCreateCouponProductValidator()
        {
            RuleFor(x => x.CouponId)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
            RuleFor(x => x.ProductIds)
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All id's must be greater than 0!");
        }
    }
}
