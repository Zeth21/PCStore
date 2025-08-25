using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.OrderValidators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator() 
        {
            RuleFor(x => x.OrderAddressId)
                .GreaterThan(0).WithMessage("Invalid address id!");
            RuleFor(x => x.OrderTotalCost)
                .GreaterThan(0).WithMessage("Invalid total cost value!");
        }
    }
}
