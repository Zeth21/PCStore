using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.ProductValidators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator() 
        {
            RuleFor(x => x.ProductPrice)
                .GreaterThan(0).WithMessage("Invalid order id!");
            RuleFor(x => x.ProductBrandId)
                .GreaterThan(0).WithMessage("Invalid brand id!");
            RuleFor(x => x.ProductCategoryId)
                .GreaterThan(0).WithMessage("Invalid category id!");
            RuleFor(x => x.ProductTypeId)
                .GreaterThan(0).WithMessage("Invalid category id!");
            RuleFor(x => x.ProductName)
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters!");
        }
    }
}
