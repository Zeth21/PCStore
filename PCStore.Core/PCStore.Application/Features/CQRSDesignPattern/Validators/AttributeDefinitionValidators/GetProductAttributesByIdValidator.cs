using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AttributeDefinitionValidators
{
    public class GetProductAttributesByIdValidator : AbstractValidator<GetProductAttributesByIdQuery>
    {
        public GetProductAttributesByIdValidator() 
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Invalid product id!");
        }
    }
}
