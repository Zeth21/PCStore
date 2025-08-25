using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountProductCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.DiscountProductValidators
{
    public class CreateDiscountProductsValidator : AbstractValidator<CreateDiscountProductsCommand>
    {
        public CreateDiscountProductsValidator() 
        {
            RuleFor(x => x.DiscountId)
                .GreaterThan(0).WithMessage("Invalid coupon id!");
            RuleFor(x => x.ProductIds)
                .Must(ids => ids.All(id => id > 0))
                .WithMessage("All id's must be greater than 0!");
        }
    }
}
