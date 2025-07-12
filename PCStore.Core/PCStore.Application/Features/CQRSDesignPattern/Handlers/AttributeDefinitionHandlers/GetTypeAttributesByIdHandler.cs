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
    public class GetTypeAttributesByIdHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<GetTypeAttributesByIdQuery, TaskListResult<GetTypeAttributesByIdResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;

        public async Task<TaskListResult<GetTypeAttributesByIdResult>> Handle(GetTypeAttributesByIdQuery request, CancellationToken cancellationToken)
        {
            var type = await _context.ProductTypes
                .Where(x => x.Id == request.TypeId)
                .FirstOrDefaultAsync(cancellationToken);
            if (type is null)
                return TaskListResult<GetTypeAttributesByIdResult>.NotFound(message: "No types found!");
            var attributes = await _context.ProductTypeAttributes
                .Include(x => x.AttributeDefinition)
                .Where(x => x.ProductTypeId == type.Id)
                .ProjectTo<GetTypeAttributesByIdResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (attributes.Count <= 0)
                return TaskListResult<GetTypeAttributesByIdResult>.NotFound(message: "No attributes found!");
            return TaskListResult<GetTypeAttributesByIdResult>.Success(message: "All attributes found successfully!", data: attributes);
        }
    }
}
