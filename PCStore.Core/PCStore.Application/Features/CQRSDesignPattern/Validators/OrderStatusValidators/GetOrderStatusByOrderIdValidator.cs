using FluentValidation;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderStatusQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Validators.OrderStatusValidators
{
    public class GetOrderStatusByOrderIdValidator : AbstractValidator<GetOrderStatusByOrderIdQuery>
    {
        public GetOrderStatusByOrderIdValidator() 
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Invalid order id!");
        }
    }
}
