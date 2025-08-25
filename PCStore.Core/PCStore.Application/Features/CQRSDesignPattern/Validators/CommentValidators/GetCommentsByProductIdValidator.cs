using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CommentQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.CommentValidators
{
    public class GetCommentsByProductIdValidator : AbstractValidator<GetCommentsByProductIdQuery>
    {
        public GetCommentsByProductIdValidator() 
        {
            RuleFor(x => x.CommentProductId)
                .GreaterThan(0).WithMessage("Invalid product id!");
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid page index number!");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Invalid page size number!");
        }
    }
}
