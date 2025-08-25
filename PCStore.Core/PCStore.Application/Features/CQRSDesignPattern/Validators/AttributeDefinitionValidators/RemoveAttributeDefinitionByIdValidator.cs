using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AttributeDefinitionValidators
{
    public class RemoveAttributeDefinitionByIdValidator : AbstractValidator<RemoveAttributeDefinitionByIdCommand>
    {
        public RemoveAttributeDefinitionByIdValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid attribute definition id!");
        }
    }
}
