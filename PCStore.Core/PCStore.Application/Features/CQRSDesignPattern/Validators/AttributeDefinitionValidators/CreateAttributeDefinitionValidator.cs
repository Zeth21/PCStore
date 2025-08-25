using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AttributeDefinitionValidators
{
    public class CreateAttributeDefinitionValidator : AbstractValidator<CreateAttributeDefinitionCommand>
    {
        public CreateAttributeDefinitionValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Attribute name cannot exceed 50 characters!")
                .NotNull().WithMessage("Attribute name is required!");
            RuleFor(x => x.DataType)
                .NotNull().WithMessage("Attribute data type is required!");
        }
    }
}
