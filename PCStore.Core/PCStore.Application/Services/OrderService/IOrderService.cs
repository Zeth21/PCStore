using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderStatusQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults;
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
        //ORDER
        Task<TaskResult<ServiceCreateOrderResult>> CreateOrder(ServiceCreateOrderCommand request, CancellationToken cancellation);
        Task<TaskResult<ServiceGetOrderDetailsByOrderIdResult>> UserGetOrderById(ServiceGetOrderDetailsByOrderIdCommand request, CancellationToken cancellation);
        Task<TaskListResult<UserGetOrderListResult>> UserGetOrderList(UserGetOrderListQuery request, CancellationToken cancellation);


        //ORDERSTATUS
        Task<TaskResult<CreateOrderStatusResult>> CreateOrderStatus(CreateOrderStatusCommand request, CancellationToken cancellation);
        Task<TaskListResult<ListGetOrderStatusByOrderIdResult>> GetOrderStatusList(ListGetOrderStatusByOrderIdQuery request, CancellationToken cancellation);
    }
}
