using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AnswerValidators
{
    public class CreateAnswerValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerValidator() 
        {
            RuleFor(x => x.AnswerText)
                .MaximumLength(200).WithMessage("Answer cannot exceed 200 characters!");
        }
    }
}
