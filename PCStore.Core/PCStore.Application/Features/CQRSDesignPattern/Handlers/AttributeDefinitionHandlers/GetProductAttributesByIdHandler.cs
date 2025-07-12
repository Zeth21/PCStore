using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AttributeDefinitionQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AttributeDefinitionHandlers
{
    class GetProductAttributesByIdHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<GetProductAttributesByIdQuery, TaskListResult<GetProductAttributesResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskListResult<GetProductAttributesResult>> Handle(GetProductAttributesByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
            if (product is null)
                return TaskListResult<GetProductAttributesResult>.NotFound(message: "Product not found!");
            var attributes = await _context.ProductAttributes
                .Include(x => x.AttributeDefinition)
                .Where(x => x.ProductId == request.ProductId)
                .ProjectTo<GetProductAttributesResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (attributes.Count <= 0)
                return TaskListResult<GetProductAttributesResult>.NotFound(message: "No attributes found!");
            return TaskListResult<GetProductAttributesResult>.Success(message: "All attributes found successfully!", data: attributes);

        }
    }
}
