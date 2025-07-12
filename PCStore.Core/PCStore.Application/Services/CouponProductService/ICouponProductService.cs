using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.CouponProductService
{
    public interface ICouponProductService
    {
        Task<TaskResult<ListCreateCouponProductResult>> CreateCouponProducts(ListCreateCouponProductCommand request, CancellationToken cancellation);
        Task<Result> RemoveCouponProducts(ListRemoveCouponProductsCommand request, CancellationToken cancellation);
    }
}
