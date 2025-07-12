using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountProductQueries
{
    public class GetAllDiscountProductsQuery : IRequest<TaskListResult<GetAllDiscountProductsResult>>
    {
        public int DiscountId { get; set; }
    }
}
