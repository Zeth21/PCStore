using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.ProductCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults;
using PCStore.Application.Features.Helpers.Helper;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.ProductHandlers
{
    public class UpdateProductTypeHandler(IMapper mapper, ProjectDbContext context,IHelperService helperService) : IRequestHandler<UpdateProductTypeCommand,TaskResult<UpdateProductTypeResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;
        private readonly IHelperService _helperService = helperService;
        public async Task<TaskResult<UpdateProductTypeResult>> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var checkProduct = await _context.Products
                .Where(x => x.ProductId == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkProduct is null)
                return TaskResult<UpdateProductTypeResult>.Fail("Product not found!");
            if (checkProduct.ProductTypeId == request.TypeId)
                return TaskResult<UpdateProductTypeResult>.Fail("Product type cannot be same as the previous one!");
            var checkType = await _context.ProductTypes
                .Where(x => x.Id == request.TypeId)
                .SingleOrDefaultAsync(cancellationToken);
            if (checkType is null)
                return TaskResult<UpdateProductTypeResult>.Fail("Type not found!");
            var dbTypeAttributes = await _context.ProductTypeAttributes
                .Where(x => x.ProductTypeId == request.TypeId)
                .Select(x => x.AttributeDefinitionId)
                .ToHashSetAsync(cancellationToken);
            var reqTypeAttributes = request.NewAttributes.Select(x => x.AttributeDefinitionId).ToHashSet();
            if (!reqTypeAttributes.SetEquals(dbTypeAttributes))
                return TaskResult<UpdateProductTypeResult>.Fail("One or more attributes are invalid for type!");
            var dataTypesDic = await _context.AttributeDefinitions
                .Where(x => reqTypeAttributes.Contains(x.Id))
                .Select(x => new 
                {
                    x.Id,
                    x.DataType
                })
                .ToDictionaryAsync(x=>x.Id,x=>x.DataType,cancellationToken);
            var newAttrList = new List<ProductAttribute>();
            foreach(var attr in request.NewAttributes) 
            {
                var isValid = _helperService.AttributeValueIsValid(attr.Value, dataTypesDic[attr.AttributeDefinitionId]);
                if (!isValid)
                    return TaskResult<UpdateProductTypeResult>.Fail($"This value, {attr.Value} is invalid for its definition!");
                var newAttr = new ProductAttribute 
                {
                    ProductId = request.ProductId,
                    AttributeDefinitionId = attr.AttributeDefinitionId,
                    Value = attr.Value
                };
                newAttrList.Add(newAttr);
            }
            try 
            {
                var oldAttributes = await _context.ProductAttributes
                    .Where(x => x.ProductId == request.ProductId)
                    .ToListAsync(cancellationToken);
                _context.RemoveRange(oldAttributes);
                await _context.AddRangeAsync(newAttrList);
                await _context.SaveChangesAsync(cancellationToken);
                var attrResultList = await _context.ProductAttributes
                    .Include(x => x.AttributeDefinition)
                    .Where(x => x.ProductId == request.ProductId)
                    .ProjectTo<CreateProductAttributesResult>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                var result = new UpdateProductTypeResult 
                {
                    TypeId = checkType.Id,
                    TypeName = checkType.Name,
                    NewAttributes = attrResultList
                };
                return TaskResult<UpdateProductTypeResult>.Success("Product type updated successfully!", data : result);
            }
            catch(Exception ex) 
            {
                return TaskResult<UpdateProductTypeResult>.Fail("Something went wrong while saving the data! " + ex.Message);
            }
        }
    }
}
