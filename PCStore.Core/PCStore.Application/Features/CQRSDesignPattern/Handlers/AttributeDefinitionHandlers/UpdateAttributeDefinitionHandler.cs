using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AttributeDefinitionHandlers
{
    public class UpdateAttributeDefinitionHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<UpdateAttributeDefinitionCommand, TaskResult<UpdateAttributeDefinitionResult>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ProjectDbContext _context = context;
        public async Task<TaskResult<UpdateAttributeDefinitionResult>> Handle(UpdateAttributeDefinitionCommand request, CancellationToken cancellationToken)
        {
            var newValues = request.Properties;
            if (newValues is null || newValues.Count == 0 || newValues.Count >= 3)
                return TaskResult<UpdateAttributeDefinitionResult>.NotFound(message: "No values found!");
            var attribute = await _context.AttributeDefinitions
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (attribute is null)
                return TaskResult<UpdateAttributeDefinitionResult>.NotFound(message: "Attribute not found!");
            try
            {
                foreach (var value in newValues)
                {
                    var property = attribute.GetType().GetProperty(value.Name);
                    if (property is null)
                        return TaskResult<UpdateAttributeDefinitionResult>.Fail(message: "One or many attributes are invalid!");
                    property.SetValue(attribute, value.Value);
                }
                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<UpdateAttributeDefinitionResult>(attribute);
                return TaskResult<UpdateAttributeDefinitionResult>.Success(message: "Attribute definition updated successfully!", data: result);
            }
            catch (Exception ex)
            {
                return TaskResult<UpdateAttributeDefinitionResult>.Fail(message: "Process failed!" + ex.Message);
            }

        }
    }
}
