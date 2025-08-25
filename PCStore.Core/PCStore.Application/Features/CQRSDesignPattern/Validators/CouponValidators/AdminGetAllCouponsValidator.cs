using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CouponValidators
{
    public class AdminGetAllCouponsValidator : AbstractValidator<AdminGetAllCouponsQuery>
    {
        public AdminGetAllCouponsValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Invalid page size value!");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Invalid page number!");
        }
    }
}
