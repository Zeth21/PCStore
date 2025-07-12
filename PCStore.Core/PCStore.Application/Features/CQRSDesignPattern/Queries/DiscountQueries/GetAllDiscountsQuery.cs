using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountQueries
{
    public class GetAllDiscountsQuery : IRequest<TaskListResult<GetAllDiscountsResult>>
    {
        public string? DiscountName { get; set; }
        public decimal? DiscountRate { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
