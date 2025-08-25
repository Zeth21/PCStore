using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AnswerQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AnswerValidators
{
    public class GetAnswersByCommentIdValidator : AbstractValidator<GetAnswersByCommentIdQuery>
    {
        public GetAnswersByCommentIdValidator()
        {
            RuleFor(x => x.CommentId)
                .GreaterThan(0).WithMessage("Invalid comment id!");
        }
    }
}
