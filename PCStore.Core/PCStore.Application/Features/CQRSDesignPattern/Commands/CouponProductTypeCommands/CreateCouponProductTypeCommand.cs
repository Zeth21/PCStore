using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductTypeResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands
{
    public class CreateCouponProductTypeCommand : IRequest<TaskResult<CreateCouponProductTypeResult>>
    {
        public int CouponId { get; set; }
        public int TypeId { get; set; }
    }
}
