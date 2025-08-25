using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.DiscountValidators
{
    public class ActiveDiscountValidator : AbstractValidator<ActiveDiscountCommand>
    {
        public ActiveDiscountValidator() 
        {
            RuleFor(x => x.DiscountId)
                .GreaterThan(0).WithMessage("Invalid discount id!");
        }
    }
}
