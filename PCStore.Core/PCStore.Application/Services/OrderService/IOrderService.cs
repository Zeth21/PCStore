using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Services.OrderService.Commands;
using PCStore.Application.Services.OrderService.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.OrderService
{
    public interface IOrderService
    {
        Task<TaskResult<ServiceCreateOrderResult>> CreateOrder(ServiceCreateOrderCommand request, CancellationToken cancellation);
        Task<TaskResult<ServiceGetOrderDetailsByOrderIdResult>> UserGetOrderById(ServiceGetOrderDetailsByOrderIdCommand request, CancellationToken cancellation);
    }
}
