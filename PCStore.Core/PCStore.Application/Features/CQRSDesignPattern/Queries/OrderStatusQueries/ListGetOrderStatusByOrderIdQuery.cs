using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.OrderStatusQueries
{
    public class ListGetOrderStatusByOrderIdQuery : IRequest<TaskListResult<ListGetOrderStatusByOrderIdResult>>
    {
        public int OrderId { get; set; }
    }
}
