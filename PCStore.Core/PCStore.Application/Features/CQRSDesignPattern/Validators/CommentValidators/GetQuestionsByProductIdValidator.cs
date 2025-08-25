using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CommentValidators
{
    public class GetQuestionsByProductIdValidator : AbstractValidator<GetQuestionsByProductIdQuery>
    {
        public GetQuestionsByProductIdValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Invalid product id!");
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid page index number!");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Invalid page size number!");
        }
    }
}
