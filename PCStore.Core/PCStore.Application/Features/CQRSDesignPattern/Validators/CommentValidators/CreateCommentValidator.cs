using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CommentCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CommentValidators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.CommentText)
                .MaximumLength(200).WithMessage("Comment text cannot exceed 200 characters!");
            RuleFor(x => x.CommentProductId)
                .GreaterThan(0).WithMessage("Invalid product id!");
        }
    }
}
