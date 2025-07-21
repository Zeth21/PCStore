using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.OrderQueries
{
    public class UserGetOrderListQuery : IRequest<TaskListResult<UserGetOrderListResult>>
    {
        public required string UserId { get; set; }
    }
}
