using AutoMapper;
using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.OrderProductListCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderProductListResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderProductListHandlers
{
    public class CreateOrderProductListHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateOrderProductListCommand, Result>
    {
        public async Task<Result> Handle(CreateOrderProductListCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderProducts.Count <= 0)
                return Result.Fail("No products found!");
            var newRecords = mapper.Map<List<OrderProductList>>(request.OrderProducts);
            foreach(var record in newRecords) 
            {
                record.OrderId = request.OrderId;
            };
            try 
            {
                await context.OrderProductLists.AddRangeAsync(newRecords,cancellationToken);
                var task = await context.SaveChangesAsync(cancellationToken);
                if (task < newRecords.Count)
                    return Result.Fail("One or more products are invalid!");
                return Result.Success("Successfull!");
            }
            catch(Exception ex) 
            {
                return Result.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
