using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.DiscountValidators
{
    public class GetAllDiscountsValidator : AbstractValidator<GetAllDiscountsQuery>
    {
        public GetAllDiscountsValidator() 
        {
            RuleFor(x => x.DiscountName)
                .MaximumLength(75)
                .When(x => x.DiscountName != null)
                .WithMessage("Discount name cannot exceed 75 characters!");
            RuleFor(x => x.DiscountRate)
                .GreaterThan(0)
                .When(x => x.DiscountRate.HasValue)
                .WithMessage("Discount rate must be greater than 0!");
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Page size must be between 1 and 100!");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0!");
        }
    }
}
