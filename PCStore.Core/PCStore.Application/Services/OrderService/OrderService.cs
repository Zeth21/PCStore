﻿using AutoMapper;
using Azure.Identity;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponUsageCommand;
using PCStore.Application.Features.CQRSDesignPattern.Commands.DiscountUsageCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderProductListCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ShoppingCartItemsCommand;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderProductListQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderStatusQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.ShoppingCartItemsQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ShoppingCartItemResults;
using PCStore.Application.Services.OrderService.Commands;
using PCStore.Application.Services.OrderService.Results;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.OrderService
{
    public class OrderService(ProjectDbContext context, IMediator mediator, IMapper mapper) : IOrderService
    {
        public async Task<TaskResult<ServiceCreateOrderResult>> CreateOrder(ServiceCreateOrderCommand request, CancellationToken cancellation)
        {
            using (var transaction = await context.Database.BeginTransactionAsync(cancellation)) 
            {
                var shopCartItemsQuery = new GetShopCartItemsQuery { UserId = request.UserId, CouponId = request.CouponId };
                var shopCartItemsResult = await mediator.Send(shopCartItemsQuery, cancellation);
                if (!shopCartItemsResult.IsSucceeded || shopCartItemsResult.Data is null)
                {
                    await transaction.DisposeAsync();
                    return TaskResult<ServiceCreateOrderResult>.Fail(shopCartItemsResult.Message ?? "ShopCartItems error!");
                }
                var orderCommand = new CreateOrderCommand
                {
                    OrderAddressId = request.AddressId,
                    UserId = request.UserId,
                    OrderTotalCost = shopCartItemsResult.Data.TotalCost
                };
                var orderResult = await mediator.Send(orderCommand, cancellation);
                if (!orderResult.IsSucceeded || orderResult.Data is null)
                {
                    await transaction.RollbackAsync(cancellation);
                    return TaskResult<ServiceCreateOrderResult>.Fail(orderResult.Message ?? "Order error!");
                }
                var orderProductListCommand = new CreateOrderProductListCommand
                {
                    OrderId = orderResult.Data.OrderId,
                    OrderProducts = mapper.Map<List<OrderProductListDTO>>(shopCartItemsResult.Data.CartItems)
                };
                var orderStatusCommand = new CreateOrderStatusCommand
                {
                    OrderId = orderResult.Data.OrderId,
                    StatusDate = orderResult.Data.OrderDate,
                    StatusNameId = 1
                };
                var discountUsageCommand = new CreateDiscountUsageCommand
                {
                    UserId = request.UserId,
                    OrderId = orderResult.Data.OrderId
                };
                var removeAllShopCartItemsCommand = new RemoveAllShopCartItemsCommand 
                {
                    UserId = request.UserId
                };
                var orderProductListResult = await mediator.Send(orderProductListCommand, cancellation);
                var orderStatusResult = await mediator.Send(orderStatusCommand, cancellation);
                var discountUsageResult = await mediator.Send(discountUsageCommand, cancellation);
                var removeAllShopCartItemsResult = await mediator.Send(removeAllShopCartItemsCommand, cancellation);
                if(!orderProductListResult.IsSucceeded || !orderStatusResult.IsSucceeded || !discountUsageResult.IsSucceeded || !removeAllShopCartItemsResult.IsSucceeded) 
                {
                    var errors = new List<string>();
                    if (!orderProductListResult.IsSucceeded)
                        errors.Add(orderProductListResult.Message ?? "OrderProductList error!");
                    if (!orderStatusResult.IsSucceeded)
                        errors.Add(orderStatusResult.Message ?? "OrderStatus error!");
                    if (!discountUsageResult.IsSucceeded)
                        errors.Add(orderProductListResult.Message ?? "DiscountUsage error!");
                    if (!removeAllShopCartItemsResult.IsSucceeded)
                        errors.Add(orderProductListResult.Message ?? "RemoveAllShopCartItems error!");
                    await transaction.RollbackAsync(cancellation);
                    return TaskResult<ServiceCreateOrderResult>.Fail("One or more errors occured!",errors:errors);
                }
                if (request.CouponId is not null && shopCartItemsResult.Data.TotalCouponDiscount is not null)
                {
                    var couponUsageCommand = new CreateCouponUsageCommand
                    {
                        CouponId = (int)request.CouponId,
                        UserId = request.UserId,
                        OrderId = orderResult.Data.OrderId,
                        DiscountTotal = (decimal)shopCartItemsResult.Data.TotalCouponDiscount
                    };
                    var couponUsageResult = await mediator.Send(couponUsageCommand, cancellation);
                    if (!couponUsageResult.IsSucceeded)
                    {
                        await transaction.RollbackAsync(cancellation);
                        return TaskResult<ServiceCreateOrderResult>.Fail("Invalid coupon!");
                    }
                }
                var result = new ServiceCreateOrderResult
                {
                    OldTotalCost = shopCartItemsResult.Data.OldTotalCost,
                    OrderAddressId = request.AddressId,
                    OrderDate = orderResult.Data.OrderDate,
                    OrderId = orderResult.Data.OrderId,
                    OrderTotalCost = shopCartItemsResult.Data.TotalCost,
                    TotalCouponDiscount = shopCartItemsResult.Data.TotalCouponDiscount,
                    TotalDiscount = shopCartItemsResult.Data.TotalDiscount,
                    StatusDate = orderStatusResult.Data!.StatusDate,
                    StatusName = orderStatusResult.Data.StatusName,
                    CartItems = mapper.Map<List<GetShopCartItemsResult>>(shopCartItemsResult.Data.CartItems)
                };
                await transaction.CommitAsync(cancellation);
                return TaskResult<ServiceCreateOrderResult>.Success("Order created successfully!",data:result);
            }
        }
        public async Task<TaskResult<ServiceGetOrderDetailsByOrderIdResult>> UserGetOrderById(ServiceGetOrderDetailsByOrderIdCommand request, CancellationToken cancellation)
        {
            var orderCommand = new GetOrderByIdQuery { OrderId = request.OrderId, UserId = request.UserId };
            var orderResult = await mediator.Send(orderCommand, cancellation);
            if (!orderResult.IsSucceeded)
                return TaskResult<ServiceGetOrderDetailsByOrderIdResult>.NotFound("Order not found!");
            var couponUsageCommand = new GetCouponUsageByOrderIdQuery { OrderId = request.OrderId };
            var orderProductsCommand = new GetOrderProductListsByOrderIdQuery { OrderId = request.OrderId };
            var orderStatusCommand = new GetOrderStatusByOrderIdQuery { OrderId = request.OrderId };

            var couponUsageResult = await mediator.Send(couponUsageCommand, cancellation);
            var orderProductsResult = await mediator.Send(orderProductsCommand, cancellation);
            var orderStatusResult = await mediator.Send(orderStatusCommand, cancellation);
            if(!orderProductsResult.IsSucceeded || !orderStatusResult.IsSucceeded) 
            {
                var errors = new List<string>();
                if (!orderProductsResult.IsSucceeded)
                    errors.Add(orderProductsResult.Message ?? "orderProducts error!");
                if (!orderStatusResult.IsSucceeded)
                    errors.Add(orderStatusResult.Message ?? "orderStatus error!");
                return TaskResult<ServiceGetOrderDetailsByOrderIdResult>.Fail("One or more errors occured!",errors:errors);
            }
            var result = new ServiceGetOrderDetailsByOrderIdResult();
            result.OrderProducts = orderProductsResult.Data!;
            mapper.Map(orderResult.Data, result);
            mapper.Map(couponUsageResult.Data, result);
            mapper.Map(orderStatusResult.Data, result);
            return TaskResult<ServiceGetOrderDetailsByOrderIdResult>.Success("Order has found successfully!",data:result);
        }

        public async Task<TaskListResult<UserGetOrderListResult>> UserGetOrderList(UserGetOrderListQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        //ORDERSTATUS
        public async Task<TaskResult<CreateOrderStatusResult>> CreateOrderStatus(CreateOrderStatusCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskListResult<ListGetOrderStatusByOrderIdResult>> GetOrderStatusList(ListGetOrderStatusByOrderIdQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }
    }
}
