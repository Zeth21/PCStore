using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponBrandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponBrandCommands
{
    public class CreateCouponBrandCommand : IRequest<TaskResult<CreateCouponBrandResult>>
    {
        public int CouponId { get; set; }
        public int BrandId { get; set; }
    }
}
