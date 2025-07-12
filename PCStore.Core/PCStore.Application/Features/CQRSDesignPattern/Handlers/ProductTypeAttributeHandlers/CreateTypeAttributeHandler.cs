using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeAttributeHandlers;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductTypeAttributeHandlers
{
    public class CreateTypeAttributeHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<CreateTypeAttributeCommand, TaskListResult<GetTypeAttributesByIdResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;

        public async Task<TaskListResult<GetTypeAttributesByIdResult>> Handle(CreateTypeAttributeCommand request, CancellationToken cancellationToken)
        {
            if (request.AttributeIds is null || request.AttributeIds.Count == 0)
                return TaskListResult<GetTypeAttributesByIdResult>.Fail(message: "No attributes found!");

            var typeExists = await _context.ProductTypes
                .FirstOrDefaultAsync(x => x.Id == request.TypeId, cancellationToken);

            if (typeExists is null)
                return TaskListResult<GetTypeAttributesByIdResult>.NotFound(message: "No types found!");

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var validAttrIds = await _context.AttributeDefinitions
                    .Where(x => request.AttributeIds.Contains(x.Id))
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);

                if (validAttrIds.Count != request.AttributeIds.Count)
                    return TaskListResult<GetTypeAttributesByIdResult>.Fail(message: "One or more attributes are invalid!");

                var existingAttrIds = await _context.ProductTypeAttributes
                    .Where(x => x.ProductTypeId == typeExists.Id)
                    .Select(x => x.AttributeDefinitionId)
                    .ToListAsync(cancellationToken);

                var newAttrIds = validAttrIds.Except(existingAttrIds).ToList();

                if (newAttrIds.Count == 0)
                    return TaskListResult<GetTypeAttributesByIdResult>.Fail(message: "All attributes are already assigned!");

                var newRelations = newAttrIds.Select(id => new ProductTypeAttribute
                {
                    ProductTypeId = typeExists.Id,
                    AttributeDefinitionId = id
                }).ToList();

                await _context.ProductTypeAttributes.AddRangeAsync(newRelations, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var insertedRelations = await _context.ProductTypeAttributes
                    .Where(x => x.ProductTypeId == typeExists.Id && newAttrIds.Contains(x.AttributeDefinitionId))
                    .Include(x => x.AttributeDefinition)
                    .ToListAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var result = insertedRelations
                    .Select(rel => _mapper.Map<GetTypeAttributesByIdResult>(rel))
                    .ToList();

                return TaskListResult<GetTypeAttributesByIdResult>.Success(
                    message: "All attributes added successfully!",
                    data: result
                );
            }
            catch (Exception ex)
            {
                try
                {
                    if (_context.Database.CurrentTransaction is not null)
                        await _context.Database.RollbackTransactionAsync(cancellationToken);
                }
                catch { }

                return TaskListResult<GetTypeAttributesByIdResult>.Fail("An error occurred: " + ex.Message);
            }
        }

    }
}
