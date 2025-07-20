using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderProductListResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.OrderProductListQueries
{
    public class GetOrderProductListsByOrderIdQuery :IRequest<TaskListResult<GetOrderProductListsByOrderIdResult>>
    {
        public int OrderId { get; set; }
    }
}
