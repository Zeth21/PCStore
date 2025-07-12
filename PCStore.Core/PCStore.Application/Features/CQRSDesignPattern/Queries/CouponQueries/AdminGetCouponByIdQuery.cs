using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries
{
    public class AdminGetCouponByIdQuery : IRequest<TaskResult<AdminGetCouponByIdResult>>
    {
        public int Id { get; set; }
    }
}
