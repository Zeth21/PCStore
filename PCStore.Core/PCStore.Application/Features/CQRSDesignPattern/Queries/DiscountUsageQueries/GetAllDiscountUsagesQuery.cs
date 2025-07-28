using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountUsageResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountUsageQueries
{
    public class GetAllDiscountUsagesQuery : IRequest<TaskListResult<GetAllDiscountUsagesResult>>
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        public bool? DiscountIsActive { get; set; }
        public bool? DiscountIsPercentage { get; set; }
        public string? DiscountName { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public bool? OrderBy { get; set; }

    }
}
