using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CommentValidators
{
    public class RemoveCommentValidator : AbstractValidator<RemoveCommentCommand>
    {
        public RemoveCommentValidator()
        {
            RuleFor(x => x.CommentId)
                .GreaterThan(0).WithMessage("Invalid comment id!");
        }
    }
}
