using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AttributeDefinitionCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AttributeDefinitionResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.AttributeDefinitionHandlers
{
    public class CreateAttributeDefinitionHandler(IMapper mapper, ProjectDbContext context) : IRequestHandler<CreateAttributeDefinitionCommand, TaskResult<CreateAttributeDefinitionResult>>
    {
        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<TaskResult<CreateAttributeDefinitionResult>> Handle(CreateAttributeDefinitionCommand request, CancellationToken cancellationToken)
        {
            var checkDef = await _context.AttributeDefinitions
                .Where(x => x.Name == request.Name && x.Unit == request.Unit)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkDef is not null)
                return TaskResult<CreateAttributeDefinitionResult>.Fail("Attribute already exists!");
            var definition = _mapper.Map<AttributeDefinition>(request);
            try
            {
                await _context.AttributeDefinitions.AddAsync(definition);
                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<CreateAttributeDefinitionResult>(definition);
                return TaskResult<CreateAttributeDefinitionResult>.Success(message: "Attribute definition created successfully!", data: result);
            }
            catch (Exception ex)
            {
                return TaskResult<CreateAttributeDefinitionResult>.Fail(message: ex.Message);
            }
        }
    }
}
