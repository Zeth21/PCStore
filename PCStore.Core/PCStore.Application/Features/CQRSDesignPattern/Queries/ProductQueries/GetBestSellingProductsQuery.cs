using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.ProductQueries
{
    public class GetBestSellingProductsQuery : IRequest<TaskListResult<GetBestSellingProductsResult>>
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

    }
}
