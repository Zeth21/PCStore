using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountProductCommand;
using PCStore.Application.Features.CQRSDesignPattern.Queries.DiscountProductQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.DiscountProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.DiscountProductService
{
    public interface IDiscountProductService
    {
        Task<TaskListResult<CreateDiscountProductResult>> CreateDiscountProducts(CreateDiscountProductsCommand request, CancellationToken cancellation);
        Task<Result> RemoveDiscountProducts(RemoveDiscountProductCommand request, CancellationToken cancellation);
        Task<TaskListResult<GetAllDiscountProductsResult>> GetAllDiscountProducts(GetAllDiscountProductsQuery request, CancellationToken cancellation);
    }
}
