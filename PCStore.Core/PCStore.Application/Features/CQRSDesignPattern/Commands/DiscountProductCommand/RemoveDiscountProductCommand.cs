using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountProductCommand
{
    public class RemoveDiscountProductCommand : IRequest<Result>
    {
        public int DiscountId { get; set; }
        public required List<int> ProductIds { get; set; }
    }
}
