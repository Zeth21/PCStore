using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.OrderStatusValidators
{
    public class RemoveOrderStatusValidator : AbstractValidator<RemoveOrderStatusCommand>
    {
        public RemoveOrderStatusValidator() 
        {
            RuleFor(x => x.StatusId)
                .GreaterThan(0).WithMessage("Invalid status id!");
        }
    }
}
