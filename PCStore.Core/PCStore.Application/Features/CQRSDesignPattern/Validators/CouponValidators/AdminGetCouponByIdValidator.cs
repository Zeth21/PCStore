using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponValidators
{
    public class AdminGetCouponByIdValidator : AbstractValidator<AdminGetCouponByIdQuery>
    {
        public AdminGetCouponByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
        }
    }
}
