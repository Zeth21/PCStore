using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductTypeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductTypeResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductTypeHandlers
{
    public class CreateProductTypeHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<CreateProductTypeCommand, TaskResult<CreateProductTypeResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;
        public async Task<TaskResult<CreateProductTypeResult>> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.AttributeIds is null || request.AttributeIds.Count == 0)
                return TaskResult<CreateProductTypeResult>.Fail(message: "There is no attributes to assign!");
            if (!request.AttributeIds.Contains(1))
                request.AttributeIds.Add(1);

            var existingType = await _context.ProductTypes
                .Where(x => x.Name == request.Name)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingType is not null)
                return TaskResult<CreateProductTypeResult>.Fail(message: "Type already exists!");

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {

                var newType = new ProductType { Name = request.Name! };
                await _context.ProductTypes.AddAsync(newType, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var validAttributeIds = await _context.AttributeDefinitions
                    .Where(a => request.AttributeIds.Contains(a.Id))
                    .ToListAsync(cancellationToken);

                if (request.AttributeIds.Count != validAttributeIds.Count)
                    return TaskResult<CreateProductTypeResult>.Fail(message: "One or more attributes are invalid!");

                var connections = validAttributeIds.Select(att => new ProductTypeAttribute
                {
                    ProductTypeId = newType.Id,
                    AttributeDefinitionId = att.Id
                }).ToList();


                await _context.ProductTypeAttributes.AddRangeAsync(connections, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var result = _mapper.Map<CreateProductTypeResult>(newType);
                foreach (var con in connections)
                {
                    var attResult = _mapper.Map<GetTypeAttributesByIdResult>(con);
                    result.Attributes.Add(attResult);
                }
                return TaskResult<CreateProductTypeResult>.Success("Product type created successfully!", result);
            }
            catch (Exception ex)
            {
                try
                {
                    await _context.Database.RollbackTransactionAsync(cancellationToken);
                }
                catch
                {

                }
                return TaskResult<CreateProductTypeResult>.Fail("An error occurred: " + ex.Message);
            }
        }

    }
}
