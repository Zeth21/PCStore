using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCategoryCommands
{
    public class RemoveCouponCategoryCommand : IRequest<Result>
    {
        public int CouponId { get; set; }
    }
}
