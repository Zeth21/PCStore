using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.OrderQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.OrderHandlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, TaskResult<GetOrderByIdResult>>
    {
        private readonly IMapper _mapper;
        private readonly ProjectDbContext _context;
        public GetOrderByIdHandler(IMapper mapper, ProjectDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<TaskResult<GetOrderByIdResult>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Where(x => x.OrderId == request.OrderId && x.OrderUserId == request.UserId)
                .ProjectTo<GetOrderByIdResult>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
            if (order is null)
                return TaskResult<GetOrderByIdResult>.NotFound("Order not found!");
            return TaskResult<GetOrderByIdResult>.Success("Order has found successfully!",order);
        }
    }
}
