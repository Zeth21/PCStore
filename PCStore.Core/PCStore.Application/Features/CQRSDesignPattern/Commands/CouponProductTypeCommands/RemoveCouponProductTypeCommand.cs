using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands
{
    public class RemoveCouponProductTypeCommand : IRequest<Result>
    {
        public int CouponId { get; set; }
    }
}
