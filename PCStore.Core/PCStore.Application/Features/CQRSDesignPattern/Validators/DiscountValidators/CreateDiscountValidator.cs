using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.DiscountValidators
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountValidator() 
        {
            RuleFor(x => x.DiscountName)
                .MaximumLength(75).WithMessage("Discount name cannot exceed 75 characters!");
            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Discount description cannot exceed 200 characters!");
            RuleFor(x => x.DiscountRate)
                .GreaterThan(0).WithMessage("Invalid coupon rate!");
        }
    }
}
