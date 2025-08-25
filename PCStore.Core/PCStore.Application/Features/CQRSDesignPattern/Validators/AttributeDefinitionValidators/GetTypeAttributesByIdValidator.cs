using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.AttributeDefinitionValidators
{
    public class GetTypeAttributesByIdValidator : AbstractValidator<GetTypeAttributesByIdQuery>
    {
        public GetTypeAttributesByIdValidator() 
        {
            RuleFor(x => x.TypeId)
                .GreaterThan(0).WithMessage("Invalid type id!");
        }
    }
}
