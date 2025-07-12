using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductAttributeCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using PCStore.Application.Features.Helpers.Helper;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductAttributeHandlers
{
    public class UpdateProductAttributesHandler(ProjectDbContext context, IMapper mapper,IHelperService helperService) : IRequestHandler<BulkUpdateProductAttributeCommand, TaskListResult<UpdateProductAttributeResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IHelperService _helperService = helperService;
        public async Task<TaskListResult<UpdateProductAttributeResult>> Handle(BulkUpdateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            if (request.Attributes.Count == 0)
                return TaskListResult<UpdateProductAttributeResult>.NotFound("No attributes were found!");
            var reqAttrIds = request.Attributes.Select(x => x.Id).ToList();
            var checkProductAttributes = await _context.ProductAttributes
                .Include(x => x.AttributeDefinition)
                .Where(x => x.ProductId == request.ProductId && reqAttrIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
            if (checkProductAttributes.Count == 0)
                return TaskListResult<UpdateProductAttributeResult>.NotFound("No attributes were found to update!");
            if (checkProductAttributes.Count != request.Attributes.Count)
                return TaskListResult<UpdateProductAttributeResult>.Fail("One or more attributes are invalid!");
            foreach(var attr in checkProductAttributes) 
            {
                var newAttr = request.Attributes
                    .Where(x => x.Id == attr.Id)
                    .SingleOrDefault();
                var dataType = attr.AttributeDefinition?.DataType;
                if (newAttr is null || dataType is null) 
                {
                    return TaskListResult<UpdateProductAttributeResult>.Fail("Attribute definition not found!");
                }
                var isValueValid = _helperService.AttributeValueIsValid(newAttr.Value, dataType);
                if (!isValueValid)
                    return TaskListResult<UpdateProductAttributeResult>.Fail($"This value {newAttr.Value} is invalid for its type");
                attr.Value = newAttr.Value;
            }
            try 
            {
                var task = await _context.SaveChangesAsync(cancellationToken);
                if (task <= 0)
                    return TaskListResult<UpdateProductAttributeResult>.Fail("Something went wrong while saving the data!");
                var result = _mapper.Map<List<UpdateProductAttributeResult>>(checkProductAttributes);
                return TaskListResult<UpdateProductAttributeResult>.Success("All attributes updated successfully!",result);
            }
            catch(Exception ex) 
            {
                return TaskListResult<UpdateProductAttributeResult>.Fail("Something went wrong! " + ex.Message);
            }
        }
    }
}
