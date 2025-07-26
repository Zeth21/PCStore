using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.BrandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.BrandQueries
{
    public class GetAllBrandsQuery : IRequest<TaskListResult<GetAllBrandsResult>>
    {
        public int PageSize { get; set; } = 50;
        public int PageNumber { get; set; } = 1;
        public string? BrandName { get; set; }
    }
}
