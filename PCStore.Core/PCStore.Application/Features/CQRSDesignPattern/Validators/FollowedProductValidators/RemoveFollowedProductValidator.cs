using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.FollowedProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.FollowedProductValidators
{
    public class RemoveFollowedProductValidator : AbstractValidator<RemoveFollowedProductCommand>
    {
        public RemoveFollowedProductValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid product id!");
        }
    }
}
