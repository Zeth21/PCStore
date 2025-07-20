using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderProductListQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderProductListResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderProductListHandlers
{
    public class GetOrderProductListsByOrderIdHandler : IRequestHandler<GetOrderProductListsByOrderIdQuery, TaskListResult<GetOrderProductListsByOrderIdResult>>
    {
        private readonly IMapper _mapper;
        private readonly ProjectDbContext _context;
        
        public GetOrderProductListsByOrderIdHandler(IMapper mapper, ProjectDbContext context) 
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<TaskListResult<GetOrderProductListsByOrderIdResult>> Handle(GetOrderProductListsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.OrderProductLists
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Brand)
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Category)
                .Where(x => x.OrderId == request.OrderId)
                .AsNoTracking()
                .ToListAsync();
            if (products.Count <= 0)
                return TaskListResult<GetOrderProductListsByOrderIdResult>.NotFound("No products has found!");
            var result = _mapper.Map<List<GetOrderProductListsByOrderIdResult>>(products);
            return TaskListResult<GetOrderProductListsByOrderIdResult>.Success("All products has found!", result);
        }
    }
}
