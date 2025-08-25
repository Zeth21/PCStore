using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.DiscountValidators
{
    public class UpdateDiscountValidator : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountValidator() 
        {
            RuleFor(x => x.DiscountId)
                .GreaterThan(0)
                .WithMessage("Invalid discount id!");

            RuleFor(x => x.DiscountName)
                .MaximumLength(75)
                .When(x => x.DiscountName != null)
                .WithMessage("Discount name cannot exceed 75 characters!");

            RuleFor(x => x.Description)
                .MaximumLength(200)
                .When(x => x.Description != null)
                .WithMessage("Discount description cannot exceed 200 characters!");

            RuleFor(x => x.DiscountRate)
                .GreaterThan(0)
                .When(x => x.DiscountRate.HasValue)
                .WithMessage("Invalid discount rate!");
        }
    }
}
