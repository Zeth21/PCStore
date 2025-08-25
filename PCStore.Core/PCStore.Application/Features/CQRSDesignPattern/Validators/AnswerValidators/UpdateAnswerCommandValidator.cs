using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AnswerValidators
{
    public class UpdateAnswerCommandValidator : AbstractValidator<UpdateAnswerCommand>
    {
        public UpdateAnswerCommandValidator() 
        {
            RuleFor(x => x.AnswerText)
                .MaximumLength(200)
                .When(x => x.AnswerText != null)
                .WithMessage("Answer cannot exceed 200 characters!");
        }
    }
}
