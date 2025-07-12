using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using PCStore.Application.Features.Helpers.Helper;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System.Globalization;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductAttributeHandlers
{
    public class CreateProductAttributesHandler(ProjectDbContext context,IHelperService helperService) : IRequestHandler<BulkCreateProductAttributesCommand, TaskListResult<CreateProductAttributesResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IHelperService _helperService = helperService;
        public async Task<TaskListResult<CreateProductAttributesResult>> Handle(BulkCreateProductAttributesCommand request, CancellationToken cancellationToken)
        {
            if (request.Attributes.Count <= 0)
                return TaskListResult<CreateProductAttributesResult>.Fail("No attributes found!");

            var typeId = await _context.Products
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => x.ProductTypeId)
                .FirstOrDefaultAsync(cancellationToken);

            var productAttributes = request.Attributes.Select(x => x.AttributeDefinitionId).ToList();

            var productAttributesSet = new HashSet<int>(productAttributes);

            var typeAttributes = await _context.ProductTypeAttributes
                .Where(x => x.ProductTypeId == typeId)
                .Select(x => x.AttributeDefinitionId)
                .ToListAsync(cancellationToken);

            var typeAttributesSet = new HashSet<int>(typeAttributes);

            if (!productAttributesSet.SetEquals(typeAttributes))
                return TaskListResult<CreateProductAttributesResult>.Fail("One or more attributes are invalid!");

            var attributeList = new List<ProductAttribute>();

            foreach (var attr in request.Attributes)
            {
                var definition = await _context.AttributeDefinitions
                    .FindAsync(attr.AttributeDefinitionId, CancellationToken.None);
                if (definition is null)
                    return TaskListResult<CreateProductAttributesResult>.Fail($"There is no attribute definition for {attr.Value}.");
                var isValueValid = _helperService.AttributeValueIsValid(attr.Value, definition.DataType);
                if (!isValueValid)
                    return TaskListResult<CreateProductAttributesResult>.Fail($"This {attr.Value} is invalid for its definition!");
                var attribute = new ProductAttribute
                {
                    ProductId = request.ProductId,
                    AttributeDefinitionId = attr.AttributeDefinitionId,
                    Value = attr.Value
                };
                attributeList.Add(attribute);
            }
            try
            {
                await _context.ProductAttributes.AddRangeAsync(attributeList, cancellationToken);
                var task = await _context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return TaskListResult<CreateProductAttributesResult>.Fail("Something went wrong while saving the data!");
                var result = new List<CreateProductAttributesResult>();
                foreach (var attribute in attributeList)
                {
                    var definition = await _context.AttributeDefinitions.FindAsync(attribute.AttributeDefinitionId, CancellationToken.None);
                    var resultAttribute = new CreateProductAttributesResult
                    {
                        Id = attribute.Id,
                        Name = definition!.Name,
                        Value = attribute.Value,
                        Unit = definition.Unit
                    };
                    result.Add(resultAttribute);
                }
                return TaskListResult<CreateProductAttributesResult>.Success(message: "All attributes created successfully!", data: result);

            }
            catch (Exception ex)
            {
                return TaskListResult<CreateProductAttributesResult>.Fail(ex.Message);
            }
        }
    }
}
