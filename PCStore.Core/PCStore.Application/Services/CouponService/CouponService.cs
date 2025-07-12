using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponBrandCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCategoryCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponQueries;
using PCStore.Application.Features.CQRSDesignPattern.Queries.CouponUsageQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Application.Services.CouponService.CouponServiceCommands;
using PCStore.Application.Services.CouponService.CouponServiceResults;
using PCStore.Domain.Enum;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCStore.Application.Services.CouponService
{
    public class CouponService(IMediator mediator,ProjectDbContext context) : ICouponService
    {
        public async Task<Result> ActiveCoupon(ActiveCouponCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request,cancellation);
            return result;
        }

        public async Task<TaskListResult<AdminGetAllCouponsResult>> AdminGetAllCoupons(AdminGetAllCouponsQuery req, CancellationToken cancellation)
        {
            var result = await mediator.Send(req, cancellation);
            return result;
        }

        public async Task<TaskResult<AdminGetCouponByIdResult>> AdminGetCouponById(AdminGetCouponByIdQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request,cancellation);
            return result;
        }

        public async Task<Result> CouponIsValid(IsCouponValidQuery req, CancellationToken cancellation)
        {
            var result = await mediator.Send(req, cancellation);
            return result;
        }

        public async Task<TaskResult<CreateCouponResult>> CreateCoupon(CreateCouponCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request,cancellation);
            return result;
        }

        public async Task<Result> DeactiveCoupon(DeactiveCouponCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request,cancellation);
            return result;
        }

        public async Task<TaskResult<ServiceUpdateCouponResult>> UpdateCoupon(ServiceUpdateCouponCommand req, CancellationToken cancellation)
        {
            using var transaction = await context.Database.BeginTransactionAsync(cancellation);
            try 
            {
                var result = new ServiceUpdateCouponResult { CouponInformation = null! };
                var couponResult = await mediator.Send(req.CouponInformation, cancellation);
                if (!couponResult.IsSucceeded)
                    return TaskResult<ServiceUpdateCouponResult>.Fail(couponResult.Message ?? "Coupon information error!");
                result.CouponInformation = couponResult.Data!;
                if(couponResult.Data!.OldTarget is null)
                {
                    result.CouponInformation = couponResult.Data;
                    await transaction.CommitAsync(cancellation);
                    return TaskResult<ServiceUpdateCouponResult>.Success("Coupon updated successfully!", data: result);
                }
                if(req.CouponInformation.CouponTargetType == CouponTargetType.ProductTypes) 
                {
                    if (req.TypeInformation is null) 
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail("Type information cannot be null!");
                    }
                    var command = new CreateCouponProductTypeCommand { CouponId = req.CouponInformation.CouponId, TypeId = req.TypeInformation.TypeId };
                    var newValues = await mediator.Send(command, cancellation);
                    if (!newValues.IsSucceeded) 
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail(newValues.Message ?? "Type information handler error!");
                    }
                    result.TypeInformation = newValues.Data;
                }
                else if (req.CouponInformation.CouponTargetType == CouponTargetType.Categories)
                {
                    if (req.CategoryInformation is null)
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail("Category information cannot be null!");
                    }
                    var command = new CreateCouponCategoryCommand { CouponId = req.CouponInformation.CouponId, CategoryId = req.CategoryInformation.CategoryId };
                    var newValues = await mediator.Send(command, cancellation);
                    if (!newValues.IsSucceeded)
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail(newValues.Message ?? "Category information handler error!");
                    }
                    result.CategoryInformation = newValues.Data;
                }
                else if (req.CouponInformation.CouponTargetType == CouponTargetType.Brands)
                {
                    if (req.BrandInformation is null)
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail("Brand information cannot be null!");
                    }
                    var command = new CreateCouponBrandCommand { CouponId = req.CouponInformation.CouponId, BrandId = req.BrandInformation.BrandId };
                    var newValues = await mediator.Send(command, cancellation);
                    if (!newValues.IsSucceeded)
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail(newValues.Message ?? "Brand information handler error!");
                    }
                    result.BrandInformation = newValues.Data;
                }
                else if (req.CouponInformation.CouponTargetType == CouponTargetType.SpecificProducts)
                {
                    if (req.ProductListInformation is null)
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail("ProductList information cannot be null!");
                    }
                    var command = new ListCreateCouponProductCommand { CouponId = req.CouponInformation.CouponId, ProductIds = req.ProductListInformation.ProductIds };
                    var newValues = await mediator.Send(command, cancellation);
                    if (!newValues.IsSucceeded)
                    {
                        await transaction.RollbackAsync();
                        return TaskResult<ServiceUpdateCouponResult>.Fail(newValues.Message ?? "Type information handler error!");
                    }
                    result.ProductListInformation = newValues.Data;
                }

                Result? removeHandler = null;
                switch(couponResult.Data.OldTarget) 
                {
                    case CouponTargetType.AllProducts:
                        removeHandler = new Result { IsSucceeded = true, Message = "No need to remove anything!", StatusCode = 200 };
                        break;
                    case CouponTargetType.SpecificProducts:
                        removeHandler = await mediator.Send(new ListRemoveCouponProductsCommand { CouponId = couponResult.Data.CouponId },cancellation);
                        break;
                    case CouponTargetType.Categories:
                        removeHandler = await mediator.Send(new RemoveCouponCategoryCommand { CouponId = couponResult.Data.CouponId }, cancellation);
                        break;
                    case CouponTargetType.Brands:
                        removeHandler = await mediator.Send(new RemoveCouponBrandCommand { CouponId = couponResult.Data.CouponId }, cancellation);
                        break;
                    case CouponTargetType.ProductTypes:
                        removeHandler = await mediator.Send(new RemoveCouponProductTypeCommand { CouponId = couponResult.Data.CouponId }, cancellation);
                        break;
                    default:
                        return TaskResult<ServiceUpdateCouponResult>.Fail("Remove handler error!");
                }
                if(removeHandler is not null && !removeHandler.IsSucceeded) 
                {
                    await transaction.RollbackAsync();
                    return TaskResult<ServiceUpdateCouponResult>.Fail(removeHandler.Message ?? "RemoveHandler error!");
                }
                await transaction.CommitAsync(cancellation);
                return TaskResult<ServiceUpdateCouponResult>.Success("Coupon updated successfully!" , data: result);
            }
            catch(Exception ex) 
            {
                await transaction.RollbackAsync(cancellation);
                return TaskResult<ServiceUpdateCouponResult>.Fail("Something went wrong! " + ex.Message);
            }
        }

    }
}
