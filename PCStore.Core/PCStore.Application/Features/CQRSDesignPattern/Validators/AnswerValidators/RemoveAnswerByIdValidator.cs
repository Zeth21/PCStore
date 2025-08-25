using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AnswerCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AnswerValidators
{
    public class RemoveAnswerByIdValidator : AbstractValidator<RemoveAnswerByIdCommand>
    {
        public RemoveAnswerByIdValidator() 
        {
            RuleFor(x => x.AnswerId)
                .GreaterThan(0).WithMessage("Invalid answer id!");
        }
    }
}
