using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands
{
    public class ListCreateCouponProductCommand : IRequest<TaskListResult<ListCreateCouponProductResult>>
    {
        public int CouponId { get; set; }
        public List<int> ProductIds { get; set; } = null!;
    }
}
