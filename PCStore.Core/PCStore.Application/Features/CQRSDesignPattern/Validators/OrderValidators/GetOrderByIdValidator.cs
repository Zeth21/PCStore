using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.OrderValidators
{
    public class GetOrderByIdValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdValidator() 
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Invalid order id!");
        }
    }
}
