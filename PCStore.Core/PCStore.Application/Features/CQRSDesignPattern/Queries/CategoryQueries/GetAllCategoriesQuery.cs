using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CategoryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.CategoryQueries
{
    public class GetAllCategoriesQuery : IRequest<TaskResult<GetAllCategoriesResult>>
    {
        public int? ParentCategoryId { get; set; } = null!;
    }
}
